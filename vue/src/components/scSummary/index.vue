<template>
    <div class="sc-summary">
        <div v-if="image" class="thumb"><img :src="image" /></div>
        <div>
            <div class="title">{{ title }}</div>
            <div class="summary">{{ summary }}</div>
        </div>
    </div>
</template>
<script>
export default {
    name: "ScSummary",
    data() {
        return {
        }
    },
    //获取子组件传过来的激活tab
    props: {
        image: { type: String, default: null },
        title: { type: String, default: "" },
        summary: { type: String, default: "" },
    },
    created() {
    },
    methods: {
    }
}
</script>
  
<style scoped>
.sc-summary {
    display: flex;
    align-items: center;
    width: 100%;
}

.sc-summary .thumb {
    width: 32px;
    height: 32px;
    background-color: lightgray;
    border-radius: var(--el-border-radius-base);
    margin-right: 10px;
}

.sc-summary .title {
    color: var(--el-text-color-primary);
    font-size: 18px;
    line-height: 1.3;
}

.sc-summary .summary {
    color: var(--el-text-color-regular);
    font-size: var(--el-font-size-base);
    line-height: 1.3;
}
</style>