<template>
    <scWaterfall>
        <template>
            <img :data-src="item.src" :alt="item.names" :style="{ 'height': item._dim + 'px', }">
            <div>
                <label>这是说明文字</label>
            </div>
        </template>
    </scWaterfall>
</template>
<script>
export default {
    data() {
        return {
            apiObj: this.$API.cmsdocarticle.page,
            list: [],
            param: {
                types_id: '0',
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
    }
}
</script>
<style scoped></style>