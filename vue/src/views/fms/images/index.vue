<template>
    <el-container>
        <el-aside width="260px" v-loading="showGrouploading">
            <el-container>
                <el-header>
                    <el-select v-model="param.types" placeholder="请选择" @change="typeChange">
                        <el-option v-for="item in options" :key="item.value" :label="item.label"
                            :value="item.value"></el-option>
                    </el-select>
                </el-header>
                <el-main class="nopadding">
                    <el-tree ref="group" class="menu" node-key="id" default-expand-all :data="group" :props="props"
                        :current-node-key="''" :highlight-current="true" :expand-on-click-node="false"
                        @node-click="groupClick">
                        <template #default="{ node, data }">
                            <span class="custom-tree-node">
                                <span class="label">{{ node.label }}</span>
                                <span class="do" v-if="data.routes">
                                    <el-icon @click.stop="remove_tree(node, data)"><el-icon-delete /></el-icon>
                                </span>
                            </span>
                        </template>
                    </el-tree>
                </el-main>
            </el-container>
        </el-aside>
        <el-container>
            <el-header>
                <div class="left-panel">
                    <el-input v-model="param.path" :style="{ width: '360px' }">
                        <template #prepend>
                            <el-icon><el-icon-folder-opened /></el-icon>
                        </template>
                        <template #append>
                            <el-button icon="el-icon-refresh" @click="refresh" />
                        </template>
                    </el-input>
                </div>
                <div class="right-panel">
                    <el-button icon="el-icon-copy-document" plain type="success" :disabled="!selectFile"
                        v-copy="copyUrl">复制地址</el-button>
                    <el-button icon="el-icon-picture" plain type="success" :disabled="!selectFile"
                        @click="lookImg">查看原图</el-button>
                    <el-button icon="el-icon-delete" plain type="danger" :disabled="!selectFile" @click="file_del" />
                    <el-button icon="el-icon-download" plain type="primary" :disabled="!selectFile" @click="file_down" />
                    <el-button icon="el-icon-upload" plain type="primary" @click="open_dialog">本地上传</el-button>
                </div>
            </el-header>
            <el-main class="nopadding" style="padding: 10px">
                <scWaterfall>
                    <template>
                        <img :data-src="item.src" :alt="item.names" :style="{ 'height': item._dim + 'px', }">
                        <div>
                            <label>这是说明文字</label>
                        </div>
                    </template>
                </scWaterfall>
            </el-main>
        </el-container>
    </el-container>
</template>
<script>
export default {
    data() {
        return {
            apiObj: this.$API.cmsdocarticle.page,
            showGrouploading: false,
            groupFilterText: "",
            group: [],
            list: [],
            param: {
                types: '0',
                row_status: '1',
                create_time: '',
                key: ''
            },
            selection: [],
            column: [
                { label: "id", prop: "id", hide: true },
                { prop: 'types', label: '类型', width: 100, formatter: this.getTypesNames },
                { prop: 'title', label: '主标题', width: 100 },
                { prop: 'qty', label: '点赞数量', width: 100 },
                { prop: 'fav_qty', label: '收藏数量', width: 100 },
                { prop: 'msg_qty', label: '留言数量', width: 100 },
                { prop: 'cat_id', label: '分类', width: 100 },
                { prop: 'author_names', label: '作者', width: 100 },
                { prop: 'row_status', label: '数据状态', width: 80 },
                { prop: 'update_names', label: '更新人员', width: 100 },
                { prop: 'update_time', label: '更新时间', width: "150", sortable: true, formatter: this.$TOOL.dateTimeFormat },
                { prop: 'create_names', label: '创建人员', width: 100 },
                { prop: 'create_time', label: '创建时间', width: "150", sortable: true, formatter: this.$TOOL.dateTimeFormat },
            ],
            row_status_list: [this.$SCM.OPTION_ALL],
            types_list: [this.$SCM.OPTION_ALL],
        };
    },
    mounted() {
        this.$SCM.list_dic(this.types_list, 'article_type', true);
    },
    methods: {
        complete() {
            this.$refs.table.refresh();
        },
        typeChange() {
        },
        groupClick() {

        },
    }
}
</script>
<style scoped></style>