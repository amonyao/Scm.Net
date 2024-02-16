<template>
    <el-container>
        <el-header>
            <div class="editor-toolbar">
                <Toolbar :editor="scEditor" :defaultConfig="toolbarConfig" :mode="mode" />
            </div>
            <div>
                <el-button type="primary" circle plain @click="newData" title="新建">
                    <sc-icon set="ms" name="new_window"></sc-icon>
                </el-button>
                <el-button type="danger" circle plain @click="saveData" title="保存">
                    <sc-icon set="ms" name="save"></sc-icon>
                </el-button>
            </div>
        </el-header>
        <el-main>
            <div class="editor-content">
                <div class="editor-container">
                    <div class="title-container">
                        <input v-model="titleHtml" :placeholder="titleConfig.placeholder" />
                    </div>
                    <Editor style="height: 900px; overflow-y: hidden;" v-model="contentHtml" :defaultConfig="editorConfig"
                        @onCreated="handleCreated" />
                </div>
            </div>
        </el-main>
    </el-container>
</template>
<script>
import '@wangeditor/editor/dist/css/style.css'

import { Editor, Toolbar } from '@wangeditor/editor-for-vue';

export default {
    components: { Editor, Toolbar },
    emits: ['new', 'save'],
    props: {
        title: { type: String, default: '' },
        content: { type: String, default: '' },
        autoSave: { type: Boolean, default: false },
        autoSaveInterval: { type: Number, default: 1000 },
        autoDetect: { type: Boolean, default: false },
        uploadApi: { type: String, default: '' },
    },
    data() {
        return {
            titleHtml: '',
            contentHtml: '',
            mode: 'simple', // 'default' 或 'simple'
            toolbarConfig: {},
            titleConfig: { placeholder: '请输入标题...' },
            editorConfig: {
                placeholder: '请输入内容...',
                MENU_CONF: {
                    // 配置上传图片
                    uploadImage: {
                        // 请求路径
                        server: this.uploadApi,
                        // 后端接收的文件名称
                        fieldName: "file",
                        maxFileSize: 1 * 1024 * 1024, // 1M
                        // 上传的图片类型
                        allowedFileTypes: ["image/*"],
                        // 小于该值就插入 base64 格式（而不上传），默认为 0
                        base64LimitSize: 1024, // 10MB
                        customInsert: this.insertSuccess,
                        // 携带的数据
                        meta: {
                            token: 'eyJhbGciOiJIUzUxMiJ9.eyJleHAiOjE2NjM0MjQ5MzEsInN1YiI6ImFkbWluIiwiaWF0IjoxNjYzNDIzMTMxOTAyfQ.McM6MZ6N9dQnAKym-9_TqAv6gjRWqf72Q4MTnMlS9AeIM-DhCjaJJrUMYbB8hs5r-HXYSXbs5O5pk9f_KUbGQg'
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
            saveInterval: null,
            scEditor: null,
            titleOld: '',
            contentOld: '',
        }
    },
    mounted() {
        this.titleHtml = this.title;
        this.contentHtml = this.content;

        if (this.autoSave) {
            this.saveInterval = setInterval(() => {
                this.saveData();
            }, this.autoSaveInterval || 1000);
        }
    },
    methods: {
        newData() {
            this.$emit('new', this.getArticle());
        },
        saveData() {
            if (this.autoDetect) {
                var changed = false;
                if (this.titleHtml != this.titleOld) {
                    this.changed = true;
                    this.titleOld = this.titleHtml;
                }
                if (this.contentHtml == this.contentOld) {
                    this.changed = true;
                    this.contentOld = this.contentHtml;
                }
                if (!changed) {
                    return;
                }
            }

            this.$emit('save', this.getArticle());
        },
        showArticle(article) {
            if (!article) {
                return;
            }

            this.titleHtml = article.title;
            this.contentHtml = article.content;
        },
        getArticle() {
            return { title: this.titleHtml, content: this.contentHtml };
        },
        handleCreated(editor) {
            this.scEditor = editor;
        },
        insertSuccess(res, insertFn) {
            if (res.code != 200 && res.success == false) {
                this.$message.error("上传文件失败，" + res.message)
                return
            }
            insertFn(this.$SCM.getUrl(res.data.url), res.data.alt, this.$SCM.getUrl(res.data.href));
        },
        uploadSuccess(file, res) {
            if (res.errno == 0) {
                this.$message.success(`${file.name} 上传成功`);
                return;
            }

            this.$message.warning(`${file.name} 上传出了点异常`)
        }
    },
    beforeUnmount() {
        var editor = this.scEditor;
        if (editor != null) {
            editor.destroy();
        }

        var interval = this.saveInterval;
        if (interval != null) {
            clearInterval(interval);
        }
    }
}
</script> 
<style type="scss" scoped>
.editor-toolbar {
    background-color: #FCFCFC;
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