<template>
    <el-container class="doc">
        <el-aside style="width: 320px;">
            <el-container>
                <el-header>
                    <el-input placeholder="输入关键字进行过滤" v-model="param.key" clearable>
                        <template #append>
                            <el-button icon="el-icon-search" @click="search()" />
                        </template>
                    </el-input>
                </el-header>
                <el-main class="doc-guid">
                    <sc-list :data="note_list" style="height: 100%;" @dropItem="itemDelete" :canSelected="true">
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
                    <div class="notepad-toolbar">
                        <Toolbar :editor="scEditor" :defaultConfig="toolbarConfig" :mode="mode" />
                    </div>
                </el-header>
                <el-main>
                    <div class="notepad-content">
                        <div class="notepad-container">
                            <div class="notepad-title">
                                <input v-model="formData.title" :placeholder="titleConfig.placeholder" />
                            </div>
                            <Editor style="height: 900px; overflow-y: hidden;" v-model="formData.content"
                                :defaultConfig="editorConfig" @onCreated="handleCreated" />
                        </div>
                    </div>
                </el-main>
                <el-footer>
                    <div class="doc-tool">
                        <el-space>
                            <label>文章分类</label>
                            <sc-select v-model="formData.cat_id" :data="cat_list"></sc-select>
                            <el-button type="primary" @click="newNote">
                                <sc-icon set="sc" name="sc-file-line"></sc-icon>新建
                            </el-button>
                            <el-button type="danger" @click="saveNote">
                                <sc-icon set="ms" name="save"></sc-icon>保存
                            </el-button>
                        </el-space>
                    </div>
                </el-footer>
            </el-container>
        </el-main>
    </el-container>
</template>

<script>
import { defineAsyncComponent } from "vue";

import '@wangeditor/editor/dist/css/style.css'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue';

export default {
    name: 'cms_doc_htmlpad',
    components: {
        Editor, Toolbar,
        Note: defineAsyncComponent(() => import("./components/note")),
    },
    data() {
        return {
            param: {
                types: 2,
                key: ''
            },//查询条件
            cat_list: [],
            note_list: [],//笔记列表
            formData: this.def_data(),
            timer: null,// 定时器
            loading: false,//加载标识
            saving: false,// 保存标识
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
                        server: this.$API.cmsdocnote.upload.url,
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
        this.listCat();
        this.search();

        this.timer = setInterval(() => {
            this.saveCache();
        }, 1000);

        this.loadCache('0');
    },
    unmounted() {
        if (this.timer) {
            clearInterval(this.timer);
        }
    },
    methods: {
        def_data() {
            return {
                id: '0',
                types: 2,
                title: '',
                content: '',
                cat_id: '0',
                ver: 1,
            }
        },
        handleCreated(editor) {
            this.scEditor = editor;
        },
        async search() {
            var res = await this.$API.cmsdocnote.list.get(this.param);
            if (!res || res.code != 200) {
                return;
            }

            this.note_list = res.data;
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
        itemClick(item) {
            if (!item) {
                return;
            }

            this.readNote(item);
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
        handleEnter() {
            this.$refs.content.focus();
        },
        async readNote(item) {
            if (!item || !item.id) {
                return;
            }

            this.loading = true;

            // 读取远程数据
            var res = await this.$API.cmsdocnote.model.get(item.id);
            if (!res || res.code != 200) {
                this.loading = false;
                return;
            }

            // 读取本地缓存
            var cahced = this.$SCM.read_json(item.id, {});

            var nativeVer = cahced.ver || 1;
            var remoteVer = res.data.ver || 1;
            if (nativeVer >= remoteVer) {
                this.formData = cahced;
            } else {
                this.formData = res.data;
            }

            this.loading = false;
        },
        newNote() {
            var tmp = this.$SCM.read_json(this.formData.id);
            var changed = false;
            if (tmp) {
                changed = tmp.id == '0' || this.formData.title != tmp.title || this.formData.content != tmp.content || this.formData.cat_id != tmp.cat_id;
            }

            if (!changed) {
                this.formData = this.def_data();
                return;
            }

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
        },
        async saveNote() {
            if (!this.formData.types) {
                this.$message.warning('请选择文章类型！');
                return;
            }

            if (!this.formData.content || this.formData.content == '<p><br></p>') {
                this.$message.warning('请输入文章内容！');
                return;
            }

            if (!this.formData.title) {
                this.formData.title = '未命名：' + this.$TOOL.dateTimeFormat(new Date());
            }

            this.saving = true;
            var res = await this.$API.cmsdocnote.save.post(this.formData);
            if (!res || res.code != 200) {
                this.$message.error(res.message);
                this.saving = false;
                return;
            }

            this.formData.id = res.data.id;
            this.formData.ver = res.data.ver;
            this.$message.success("数据已保存！");

            // 暂不删除本地
            // this.$SCM.drop_cache(this.formData.id);

            this.saving = false;

            this.search();
        },
        loadCache(id) {
            this.formData = this.$SCM.read_json('htmlpad_' + id);
            if (!this.formData) {
                this.formData = this.def_data();
            }
        },
        saveCache() {
            if (this.saving) {
                return;
            }

			this.$SCM.save_cache('htmlpad_' + this.formData.id, this.formData);
        },
    }
}
</script>
<style type="scss" scoped>
.doc {
    .doc-guid {
        background-color: var(--el-bg-color);
        padding: 0px;
    }

    .doc-tool {
        text-align: center;
        align-items: center;
        margin-top: -4px;
    }

    .notepad-toolbar {
        margin: 0 auto;
        height: 100%;
        overflow: hidden;
    }

    .notepad-content {
        overflow-y: auto;
        position: relative;

        .notepad-container {
            width: 860px;
            margin: 0 auto;
            background-color: var(--el-bg-color);
            padding: 20px 50px 50px 50px;
            border: 1px solid var(--el-border-color-light);
            box-shadow: var(--el-box-shadow-light);
        }

        .notepad-title {
            padding: 20px 0;
            border-bottom: 1px solid var(--el-border-color-light);

            input {
                font-size: 30px;
                border: 0;
                outline: none;
                width: 100%;
                line-height: 1;
            }
        }
    }
}
</style>
