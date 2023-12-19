<template>
    <el-container>
        <el-aside width="220px" v-loading="showGrouploading">
            <sc-list :data="group"></sc-list>
        </el-aside>
        <el-container>
            <el-main class="nopadding">
                <el-tabs v-model="activeName" @tab-click="handleClick">
                    <el-tab-pane label="1" name="first">推荐</el-tab-pane>
                    <el-tab-pane label="2" name="second">最新</el-tab-pane>
                    <el-tab-pane label="3" name="third">最热</el-tab-pane>
                </el-tabs>
                <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
                    <li v-for="i in count" :key="i" class="infinite-list-item">{{ i }}</li>
                </ul>
            </el-main>
        </el-container>
    </el-container>
</template>
<script>
export default {
    components: {
    },
    data() {
        return {
            apiObj: this.$API.uruser.page,
            showGrouploading: false,
            group: [],
            list: [],
            param: {
                key: "",
            },
            activeName: '',
        };
    },
    mounted() {
    },
    methods: {
        async load() {

        },
    },
};
</script>
