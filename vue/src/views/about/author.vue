<template>
    <el-card>
        <div style="padding: 20px">
            <span>暂时还没有来得及写，如果您有好意见或建议，请先通QQ交流群沟通。</span>
        </div>
        <img :src="img" class="image" />
    </el-card>
</template>

<script>
export default {
    data() {
        return {
            img: this.$CONFIG.SERVER_URL + '/data/qq.jpg',
        }
    },
    mounted() {
    },
    methods: {
    }
}
</script>
<style scoped>
.el-card {
    width: 600px;
    margin: 0 auto;
}

.image {
    width: 100%;
}
</style>
