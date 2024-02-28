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
                        <el-card>
                            <div id="editor"></div>
                        </el-card>
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
import { defineAsyncComponent } from "vue";

import EditorJS from '@editorjs/editorjs';
import Header from '@editorjs/header';
import List from '@editorjs/list';

export default {
    components: {
        Note: defineAsyncComponent(() => import("./components/note")),
    },
    data() {
        return {
            param: {
                types: 4,
                key: ''
            },
            cat_id: '0',
            cat_list: [],
            loading: false,
            article_list: [],
            editor: null,
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
                types: 4,
                cat_id: '0',
                title: '',
                content: '',
            }
        },
        initEditor() {
            this.editor = new EditorJS({
                holder: 'editor',
                autofocus: true,
                placeholder: 'Press Tab',
                tools: {
                    header: Header,
                    list: List
                },
                i18n: {
                    messages: {
                        ui: {
                            "blockTunes": {
                                "toggler": {
                                    "Click to tune": "点击调整",
                                    "or drag to move": "或拖拽移动"
                                },
                            },
                            "inlineToolbar": {
                                "converter": {
                                    "Convert to": "转换为"
                                }
                            },
                            "toolbar": {
                                "toolbox": {
                                    "Add": "点击添加"
                                }
                            },
                            'popover': {
                                'Filter': '过滤',
                                'Nothing found': '未找到'
                            }
                        },
                        toolNames: {
                            "Text": "文本",
                            "Heading": "标题",
                            "List": "列表",
                            "Warning": "Примечание",
                            "Checklist": "Чеклист",
                            "Quote": "Цитата",
                            "Code": "Код",
                            "Delimiter": "Разделитель",
                            "Raw HTML": "HTML-фрагмент",
                            "Table": "Таблица",
                            "Link": "Ссылка",
                            "Marker": "Маркер",
                            "Bold": "加粗",
                            "Italic": "斜体",
                            "InlineCode": "Моноширинный"
                        },
                        tools: {
                            "warning": { // <-- 'Warning' tool will accept this dictionary section
                                "Title": "Название",
                                "Message": "Сообщение",
                            },

                            /**
                             * Link is the internal Inline Tool
                             */
                            "link": {
                                "Add a link": "Вставьте ссылку"
                            },
                            /**
                             * The "stub" is an internal block tool, used to fit blocks that does not have the corresponded plugin
                             */
                            "stub": {
                                'The block can not be displayed correctly.': 'Блок не может быть отображен'
                            },
                            'paragraph': {
                                'Press Tab': '输入内容'
                            },
                        },
                        blockTunes: {
                            'delete': {
                                'Delete': '删除'
                            },
                            'moveUp': {
                                'Move up': '上移'
                            },
                            'moveDown': {
                                'Move down': '下移'
                            },
                        }
                    }
                }
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
            var obj = JSON.parse(this.formData.content);
            this.editor.render(obj);
        },
        newNote() {
        },
        async saveNote() {
            var content = await this.editor.save();
            this.formData.cat_id = this.cat_id;
            this.formData.title = 'PAD_' + (new Date().getTime());
            this.formData.content = JSON.stringify(content);

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