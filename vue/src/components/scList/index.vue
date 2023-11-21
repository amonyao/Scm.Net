<template>
    <div class="sc-list">
        <div v-for="(item, index) in data" @click="itemClick(item, index)" :key="index" class="sc-list-item"
            :class="canSelected && selectedIndex == index ? 'active' : ''">
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
    name: "ScList",
    emits: ["change"],
    data() {
        return {
            selectedIndex: -1,
            selectedItem: null,
        }
    },
    //获取子组件传过来的激活tab
    props: {
        data: { type: Array, default: function () { return []; } },
        result: { type: Object, default: () => { } },
        width: { type: String, default: "" },
        canSelected: { type: Boolean, default: true },
    },
    created() {
    },
    methods: {
        getWidth() {
            if (this.width) {
                return "width:" + this.width
            }
        },
        itemClick(item, index) {
            if (this.selectedIndex == index) {
                return;
            }

            this.selectedIndex = index;
            this.$emit("change", item);
        }
    }
}
</script>
  
<style scoped>
.sc-list {}

.sc-list-item {
    padding: 10px;
    border-bottom: dashed 1px #ccc;
}

.sc-list .active {
    background-color: lightblue;
}
</style>