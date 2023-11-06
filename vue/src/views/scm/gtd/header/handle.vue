<template>
    <el-card :header="header">
        <div style="overflow: scroll;">
            <div v-for="(item, index) in list" @click="onRightClick(item)" :key="index" class="list-item">
                <div class="list-item-text">
                    <el-checkbox></el-checkbox>
                    {{ item.title }}
                </div>
                <div>结束时间</div>
                <img :src="item.icon" alt="" class="cm-img-03" v-if="item.icon">
            </div>
        </div>
    </el-card>
</template>
<script>
export default {
    props: {
        apiObj: { type: Object, default: () => { } },
        handle: { type: Number, default: 0 },
        header: { type: String, default: '' },
        height: { type: String, default: '' }
    },
    data() {
        return {
            list: [],
        };
    },
    mounted() {
        this.reload();
    },
    methods: {
        async getData() {
            // if (!this.apiObj) {
            //     return;
            // }

            var res = await this.$API.scmgtdheader.list.get({});
            if (!res || res.code != 200) {
                return;
            }

            this.list = res.data;
        },
        reload() {
            this.getData();
        },
        onRightClick(item) {
            if (item.onRightClick) {
                item.onRightClick();
            }
        }
    },
};
</script>

<style>
.list-item {
    padding: 10px;
    border-bottom: dashed 1px #ccc;
}
</style>