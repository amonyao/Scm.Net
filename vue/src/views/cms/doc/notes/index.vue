<template>
    <el-container class="doc">
        <el-aside>
            <el-container>
                <el-header>
                    <el-input placeholder="输入关键字进行过滤" v-model="param.key" clearable></el-input>
                    <el-button type="primary" round icon="el-icon-search" class="add-column" @click="search()"></el-button>
                </el-header>
                <el-main class="guid">
                    <div class="list">
                        <div v-for="item in article_list" @click="itemClick(item)" :key="item.id" class="list-item"
                            :class="{ active: item.id == this.formData.id }">
                            <div class="title">{{ item.title }}</div>
                            <div class="sub-title">{{ item.sub_title }}</div>
                            <div class="time">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
                        </div>
                    </div>
                </el-main>
            </el-container>
        </el-aside>
        <el-main style="padding: 0px;">
            <el-container>
                <el-header>
                    <div class="toolbar"></div>
                </el-header>
                <el-main>
                    <div class="box">
                        <div class="tit"><input v-model="formData.title" placeholder="请输入标题……" @change="changeTitle()" />
                        </div>
                        <Editor ref="editor" v-model="formData.content" :init="editor_config" class="doc-cnt"
                            id="tinydemo-doc" api-key="jh10jljrhxrbvc2sbwx9maq9x1y1dy5yatkmh2tnv6gcd9gp" />
                    </div>
                </el-main>
            </el-container>
        </el-main>
    </el-container>
</template>
<script>
import Editor from '@tinymce/tinymce-vue'
export default {
    components: {
        Editor,
    },
    data() {
        return {
            param: {
                key: ''
            },
            formData: this.def_data(),
            article_list: [],
            editor_config: {
                language: 'zh_CN',
                menubar: false,
                inline: true,
                toolbar: 'newdocument save | undo redo restoredraft | cut copy | forecolor backcolor bold italic underline strikethrough link | alignleft aligncenter alignright alignjustify | bullist numlist blockquote subscript superscript removeformat | \
                    formatselect fontselect fontsizeselect | \
                    table image media charmap emoticons hr pagebreak print preview | fullscreen | bdmap indent2em lineheight formatpainter axupimgs | code',
                fixed_toolbar_container: '.doc .toolbar',
                custom_ui_selector: 'body',
                placeholder: 'placeholder占位文字',
                //auto_focus: true,
                toolbar_mode: 'floating',
                toolbar_sticky: true,
                plugins: 'code image save fullscreen print preview',
                autosave_ask_before_unload: false,
                fontsize_formats: '12px 14px 16px 18px 24px 36px 48px 56px 72px',
                font_formats: '微软雅黑=Microsoft YaHei,Helvetica Neue,PingFang SC,sans-serif;苹果苹方=PingFang SC,Microsoft YaHei,sans-serif;宋体=simsun,serif;仿宋体=FangSong,serif;黑体=SimHei,sans-serif;Arial=arial,helvetica,sans-serif;Arial Black=arial black,avant garde;Book Antiqua=book antiqua,palatino;Comic Sans MS=comic sans ms,sans-serif;Courier New=courier new,courier;Georgia=georgia,palatino;Helvetica=helvetica;Impact=impact,chicago;Symbol=symbol;Tahoma=tahoma,arial,helvetica,sans-serif;Terminal=terminal,monaco;Times New Roman=times new roman,times;Verdana=verdana,geneva;Webdings=webdings;Wingdings=wingdings,zapf dingbats;知乎配置=BlinkMacSystemFont, Helvetica Neue, PingFang SC, Microsoft YaHei, Source Han Sans SC, Noto Sans CJK SC, WenQuanYi Micro Hei, sans-serif;小米配置=Helvetica Neue,Helvetica,Arial,Microsoft Yahei,Hiragino Sans GB,Heiti SC,WenQuanYi Micro Hei,sans-serif',
                images_upload_url: `${this.$CONFIG.API_URL}/cmsdocnotes/upload`,
                images_upload_base_path: this.$CONFIG.SERVER_URL,
                // images_upload_handler: function (blobInfo, succFun, failFun) {
                //     succFun('/demo/images/img.jpg');
                //     if (failFun) {
                //         failFun();
                //     }
                // },
                init_instance_callback: function (editor) {
                    //editor.fadeIn(1500);
                    editor.execCommand('selectAll');
                    editor.selection.getRng().collapse(false);
                    editor.focus();
                },
                save_onsavecallback: this.saveArticle
            },
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        init() {
            this.search();
        },
        def_data() {
            return {
                id: '0',
                title: '',
                content: '',
            };
        },
        async search() {
            var res = await this.$API.cmsdocnotes.list.get(this.param);
            if (!res || res.code != 200) {
                return;
            }

            this.article_list = res.data;
        },
        changeTitle() {
            //this.$refs.editor.focus();
        },
        async saveArticle() {
            if (!this.formData.title) {
                this.formData.title = '未命名：' + this.$TOOL.dateTimeFormat(new Date());
            }

            var res = await this.$API.cmsdocnotes.save.post(this.formData);
            if (!res || res.code != 200) {
                return;
            }

            this.formData = this.def_data();
            this.init();
        },
        itemClick(item) {
            if (!item) {
                return;
            }

            this.readArticle(item.id);
        },
        async readArticle(id) {
            if (!id || id === '0') {
                return;
            }

            var res = await this.$API.cmsdocnotes.model.get(id);
            if (!res || res.code != 200) {
                return;
            }

            this.formData = res.data;
        }
    }
}
</script>
<style>
.doc {}

.doc .box {
    padding: 10px;
    border: 1px solid #ddd;
    background-color: white;
    margin: 0 auto;
    max-width: 595pt;
    height: 842pt;
}

.doc .tit {
    padding-bottom: 10px;
}

.doc .tit input {
    font-size: 24px;
    padding: 8px 8px;
    width: 100%;
    box-sizing: border-box;
    border: none;
    box-shadow: none;
    background-color: #fefefe;
    color: #777;
    border-bottom: 1px dotted #ccc;
}

.doc .doc-cnt {
    min-height: 300px;
    max-height: 500px;
    overflow: auto;
    font-size: 16px;
}

.doc .doc-cnt:focus {
    outline: 0;
}

.doc .doc-cnt p {
    text-indent: 0;
}

.doc .guid {
    background-color: var(--el-bg-color);
}

.doc .list {
    overflow: scroll;
    background: var(--el-bg-color);
}

.doc .list .active {
    background-color: dodgerblue;
    border-bottom-color: var(--el-bg-color);
}

.doc .list-item {
    padding: 10px;
    border-bottom: 1px dashed #eeee;
}

.doc .list-item:hover {
    background-color: lightskyblue;
    border-bottom-color: var(--el-bg-color);
}
</style>