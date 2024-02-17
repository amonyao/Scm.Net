<template>
    <el-container class="doc">
        <el-aside style="width: 320px;">
            <el-container>
                <el-header>
                    <el-input placeholder="输入关键字进行过滤" v-model="param.key" clearable>
                        <template #prepend>
                            <sc-select v-model="param.types" :data="types_list" style="width: 115px"></sc-select>
                        </template>
                        <template #append>
                            <el-button icon="el-icon-search" @click="search()" />
                        </template>
                    </el-input>
                </el-header>
                <el-main class="guid">
                    <sc-list :data="article_list" style="height: 100%;" @dropItem="itemDelete">
                        <template #item="{ item }">
                            <sc-summary :title="item.title" :summary="this.$TOOL.dateTimeFormat(item.create_time)"
                                @click="itemClick(item)"></sc-summary>
                        </template>
                    </sc-list>
                </el-main>
            </el-container>
        </el-aside>
        <el-main style="padding: 0px;">
            <el-container>
                <el-header>
                    <div class="editor-toolbar">
                        <Toolbar :editor="scEditor" :defaultConfig="toolbarConfig" :mode="mode" />
                    </div>
                </el-header>
                <el-main>
                    <div class="editor-content">
                        <div class="editor-container">
                            <div class="title-container">
                                <input v-model="title" :placeholder="titleConfig.placeholder" />
                            </div>
                            <Editor style="height: 900px; overflow-y: hidden;" v-model="content"
                                :defaultConfig="editorConfig" @onCreated="handleCreated" />
                        </div>
                    </div>
                </el-main>
                <el-footer>
                    <div class="editor-footbar">
                        <el-space>
                            <label>文章类型</label>
                            <sc-select v-model="formData.types" :data="types_list"></sc-select>
                            <label>文章分类</label>
                            <sc-select v-model="formData.cat_id" :data="cat_list"></sc-select>
                            <el-button type="primary" @click="newArticle">
                                <sc-icon set="sc" name="sc-file-line"></sc-icon>
                                新建
                            </el-button>
                            <el-button type="danger" @click="saveArticle">
                                <sc-icon set="ms" name="save"></sc-icon>
                                保存
                            </el-button>
                        </el-space>
                    </div>
                </el-footer>
            </el-container>
        </el-main>
    </el-container>
</template>
<script>
import '@wangeditor/editor/dist/css/style.css'

import { Editor, Toolbar } from '@wangeditor/editor-for-vue';

export default {
    components: { Editor, Toolbar },
    data() {
        return {
            param: {
                types: 0,
                key: ''
            },
            title: '',
            content: '',
            formData: this.def_data(),
            types_list: [],
            cat_list: [],
            article_list: [],
            loading: false,
            scEditor: null,
            mode: 'simple', // 'default' 或 'simple'
            toolbarConfig: {},
            titleConfig: { placeholder: '请输入标题...' },
            editorConfig: {
                placeholder: '请输入内容...',
                MENU_CONF: {
                    // 配置上传图片
                    uploadImage: {
                        // 请求路径
                        server: this.$API.cmsdocnotes.upload.url,
                        // 后端接收的文件名称
                        fieldName: "file",
                        maxFileSize: 1 * 1024 * 1024, // 1M
                        // 上传的图片类型
                        allowedFileTypes: ["image/*"],
                        // 小于该值就插入 base64 格式（而不上传），默认为 0
                        base64LimitSize: 1024, // 10MB
                        customInsert: this.insertSuccess,
                        // 携带的数据
                        header: {
                            token: ''
                        },
                        // 单个文件上传成功之后
                        onSuccess: this.uploadSuccess,
                        // 单个文件上传失败
                        onFailed(file, res) {
                            console.log(res)
                            this.$message.error(`${file.name} 上传失败`)
                        },
                        // 上传错误，或者触发 timeout 超时
                        onError(file, err, res) {
                            console.log(err, res)
                            this.$message.error(`${file.name} 上传出错`)
                        },
                    }
                }
            },
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        init() {
            this.types_list.push({ id: '0', value: 0, label: '请选择' });
            this.types_list.push({ id: '1', value: 50, label: '笔记' });
            this.types_list.push({ id: '2', value: 60, label: '博客' });

            this.search();
            this.listCat();
        },
        def_data() {
            return {
                id: '0',
                types: 0,
                cat_id: '0',
                title: '',
                content: '',
            };
        },
        handleCreated(editor) {
            this.scEditor = editor;
        },
        async search() {
            var res = await this.$API.cmsdocnotes.list.get(this.param);
            if (!res || res.code != 200) {
                return;
            }

            this.article_list = res.data;
        },
        async listCat() {
            var res = await this.$API.cmsrescat.list.get();
            if (!res || res.code != 200) {
                return;
            }

            this.cat_list.push({ id: '0', value: '0', label: '默认' });
            res.data.forEach(element => {
                this.cat_list.push({ id: element.id, value: element.id, label: element.namec });
            });
        },
        newArticle() {
            var changed = this.formData.title != this.title || this.formData.content != this.content;
            if (changed) {
                this.$confirm(`您有数据未保存，确认要新建文档吗？`, "提示", {
                    type: "warning",
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                })
                    .then(() => {
                        this.formData = this.def_data();
                        this.showArticle();
                    })
                    .catch(() => { });
                return;
            }

            this.formData = this.def_data();
            this.showArticle();
        },
        async saveArticle() {
            if (!this.content || this.content == '<p><br></p>') {
                this.$message.warning('请输入文章内容！');
                return;
            }

            if (!this.formData.types) {
                this.$message.warning('请选择文章类型！');
                return;
            }

            this.formData.title = this.title;
            this.formData.content = this.content;

            if (!this.formData.title) {
                this.formData.title = '未命名：' + this.$TOOL.dateTimeFormat(new Date());
            }

            var res = await this.$API.cmsdocnotes.save.post(this.formData);
            if (!res || res.code != 200) {
                this.$message.error(res.message);
                return;
            }

            this.formData.id = res.data.data;
            this.$message.success("数据已保存！");
            this.search();
        },
        itemClick(item) {
            if (!item) {
                return;
            }

            this.readArticle(item.id);
        },
        itemDelete(item) {
            if (!item || !item.id) {
                return;
            }

            this.$confirm(
                `确定要删除文章 ${item.title} 吗？`,
                "提示",
                {
                    type: "warning",
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                }
            ).then(async () => {
                var loading = this.$loading();
                var res = await this.$API.cmsdocnotes.status.post({ 'ids': [item.id], 'status': 2 });
                if (!res || res.code != 200) {
                    this.$alert(res.message, "提示", { type: "error" });
                    return;
                }

                loading.close();
                this.search();
            }).catch(() => { });
        },
        async readArticle(id) {
            if (!id || id === '0') {
                return;
            }

            this.loading = true;
            var res = await this.$API.cmsdocnotes.model.get(id);
            if (!res || res.code != 200) {
                this.loading = false;
                return;
            }

            this.formData = res.data;
            this.showArticle();
        },
        showArticle() {
            this.title = this.formData.title;
            this.content = this.formData.content;
        }
    }
}
</script>
<style type="scss" scoped>
.doc {
    .guid {
        background-color: var(--el-bg-color);
        padding: 0px;
    }

    .editor-toolbar {
        background-color: #FCFCFC;
        margin: 0 auto;
        height: 100%;
        overflow: hidden;
    }

    .editor-content {
        overflow-y: auto;
        position: relative;

        .editor-container {
            width: 850px;
            margin: 10px auto;
            background-color: #fff;
            padding: 20px 50px 50px 50px;
            border: 1px solid #e8e8e8;
            box-shadow: 0 2px 10px rgb(0 0 0 / 12%);
        }

        .title-container {
            padding: 20px 0;
            border-bottom: 1px solid #e8e8e8;

            input {
                font-size: 30px;
                border: 0;
                outline: none;
                width: 100%;
                line-height: 1;
            }
        }
    }

    .editor-footbar {
        text-align: center;
        align-items: center;
    }
}
</style>
