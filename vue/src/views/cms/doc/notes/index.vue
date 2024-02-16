<template>
    <el-container class="doc">
        <el-aside>
            <el-container>
                <el-header>
                    <el-input placeholder="输入关键字进行过滤" v-model="param.key" clearable></el-input>
                    <el-button type="primary" circle icon="el-icon-search" class="add-column" @click="search()"></el-button>
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
            <scEditor ref="editor" @new="newArticle" @save="saveArticle" :uploadApi="uploadApi" />
        </el-main>
    </el-container>
</template>
<script>
import scEditor from "@/components/scEditor/doc";

export default {
    components: {
        scEditor,
    },
    data() {
        return {
            param: {
                key: ''
            },
            formData: this.def_data(),
            article_list: [],
            loading: false,
            uploadApi: this.$API.cmsdocnotes.upload.url
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
        newArticle(article) {
            var changed = this.formData.title != article.title || this.formData.content != article.content;
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
        async saveArticle(article) {
            this.formData.title = article.title;
            this.formData.content = article.content;

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
                if (res.code == 200) {
                    loading.close();
                    this.search();
                } else {
                    this.$alert(res.message, "提示", { type: "error" });
                }
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
            var editor = this.$refs.editor;
            if (editor) {
                editor.showArticle(this.formData);
            }
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
}
</style>
