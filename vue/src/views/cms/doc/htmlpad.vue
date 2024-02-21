<template>
    <Note ref="scNote" @readNote="readNote" @newNote="newNote" @saveNote="saveNote">
        <template #header>
            <div class="editor-toolbar">
                <Toolbar :editor="scEditor" :defaultConfig="toolbarConfig" :mode="mode" />
            </div>
        </template>
        <template #main>
            <div class="editor-content">
                <div class="editor-container">
                    <div class="title-container">
                        <input v-model="title" :placeholder="titleConfig.placeholder" />
                    </div>
                    <Editor style="height: 900px; overflow-y: hidden;" v-model="content" :defaultConfig="editorConfig"
                        @onCreated="handleCreated" />
                </div>
            </div>
        </template>
    </Note>
</template>
<script>
import { defineAsyncComponent } from "vue";

import '@wangeditor/editor/dist/css/style.css'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue';

export default {
    components: {
        Editor, Toolbar,
        Note: defineAsyncComponent(() => import("./components/note")),
    },
    data() {
        return {
            title: '',
            content: '',
            formData: this.def_data(),
            article_list: [],
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
    },
    methods: {
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
        newNote() {
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
        async saveNote() {
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
        async readNote(item) {
            if (!item || item.id === '0') {
                return;
            }

            this.loading = true;
            var res = await this.$API.cmsdocnotes.model.get(item.id);
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
</style>
