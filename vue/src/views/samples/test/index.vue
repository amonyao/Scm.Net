<template>
    <el-container>
        <el-button @click="myShow">测试</el-button>
    </el-container>
</template>
<script>
export default {
    data() {
        return {
        };
    },
    mounted() { },
    methods: {
        async myShow() {
            //this.$TOOL.showNotify({ title: '测试' });
            this.$TOOL.showTitle({ title: '这是一个很长很长的测试，Hello！' })
        },
    },
};
</script>
