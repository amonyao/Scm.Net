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
                <el-main class="guid">
                    <sc-list :data="article_list" style="height: 100%;" @dropItem="itemDelete" :canSelected="true">
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
                <el-main>
                    <div class="richpad">
                        <v-md-editor v-model="formData.content" height="800px"></v-md-editor>
                    </div>
                </el-main>
                <el-footer>
                    <div class="footbar">
                        <el-space>
                            <label>文章分类</label>
                            <sc-select v-model="cat_id" :data="cat_list"></sc-select>
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

import VMdEditor from '@kangc/v-md-editor';
import '@kangc/v-md-editor/lib/style/base-editor.css';
import githubTheme from '@kangc/v-md-editor/lib/theme/github.js';
import '@kangc/v-md-editor/lib/theme/style/github.css';

import hljs from 'highlight.js';

// // codemirror 编辑器的相关资源
// import Codemirror from 'codemirror';
// // mode
// import 'codemirror/mode/markdown/markdown';
// import 'codemirror/mode/javascript/javascript';
// import 'codemirror/mode/css/css';
// import 'codemirror/mode/htmlmixed/htmlmixed';
// import 'codemirror/mode/vue/vue';
// // edit
// import 'codemirror/addon/edit/closebrackets';
// import 'codemirror/addon/edit/closetag';
// import 'codemirror/addon/edit/matchbrackets';
// // placeholder
// import 'codemirror/addon/display/placeholder';
// // active-line
// import 'codemirror/addon/selection/active-line';
// // scrollbar
// import 'codemirror/addon/scroll/simplescrollbars';
// import 'codemirror/addon/scroll/simplescrollbars.css';
// // style
// import 'codemirror/lib/codemirror.css';

export default {
    name: 'cms_doc_markpad',
    components: {
        VMdEditor
    },
    data() {
        return {
            param: {
                types: 3,
                key: ''
            },
            cat_id: '0',
            cat_list: [],
            loading: false,
            article_list: [],
            formData: this.def_data()
        };
    },
    mounted() {
        this.listCat();
        this.search();
        this.initEditor();
    },
    methods: {
        def_data() {
            return {
                id: '0',
                types: 3,
                cat_id: '0',
                title: '',
                content: '',
            }
        },
        initEditor() {
            //VMdEditor.Codemirror = Codemirror;
            VMdEditor.use(githubTheme, {
                Hljs: hljs,
            });
        },
        async search() {
            var res = await this.$API.cmsdocnote.list.get(this.param);
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
        async readNote(item) {
            if (!item || !item.id) {
                return;
            }

            this.loading = true;
            var res = await this.$API.cmsdocnote.model.get(item.id);
            if (!res || res.code != 200) {
                this.loading = false;
                return;
            }

            this.formData = res.data;
        },
        newNote() {
            this.formData = this.def_data();
        },
        async saveNote() {
            this.formData.cat_id = this.cat_id;
            this.formData.title = 'PAD' + (new Date().getTime());

            var res = await this.$API.cmsdocnote.save.post(this.formData);
            if (!res || res.code != 200) {
                return;
            }

            this.search();
        }
    },
};
</script>
<style type="scss" scoped>
.doc {
    .guid {
        background-color: var(--el-bg-color);
        padding: 0px;
    }

    .richpad {
        width: 860px;
        min-height: 800px;
        margin: 0 auto;

        .el-card {
            min-height: 800px;
        }
    }

    .footbar {
        text-align: center;
        align-items: center;
        margin-top: -4px;
    }
}
</style>