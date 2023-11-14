<template>
    <div>
        <div v-for="(item, index) in data" @click="itemClick(item)" :key="index" class="list-item">
            <slot name="item" :item="item" :index="index">
                <div class="list-item-text">
                    <el-checkbox></el-checkbox>
                    {{ item.title }}
                </div>
                <div>结束时间</div>
                <img :src="item.icon" alt="" class="cm-img-03" v-if="item.icon">
            </slot>
        </div>
    </div>
</template>
<script>
export default {
    name: "ElList",
    emits: ["change"],
    data() {
        return {
        }
    },
    //获取子组件传过来的激活tab
    props: {
        data: { type: Array, default: function () { return []; } },
        result: { type: Object, default: () => { } },
        width: { type: String, default: "" }
    },
    created() {
    },
    methods: {
        getWidth() {
            if (this.width) {
                return "width:" + this.width
            }
        },
        itemClick(item) {
            // if (item.onRightClick) {
            //     item.onRightClick();
            // }
            this.$emit("change", item);
        }
    }
}
</script>
  
<style scoped>
.list-item {
    padding: 10px;
    border-bottom: dashed 1px #ccc;
}
</style>