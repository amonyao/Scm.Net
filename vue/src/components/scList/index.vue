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
            <span class="opt" v-if="!hideDo">
                <el-icon @click.stop="itemEdit(item, index)" v-if="!hideEdit" title="编辑"><el-icon-edit /></el-icon>
                <el-icon @click.stop="itemRemove(item, index)" v-if="!hideRemove" title="删除"><el-icon-delete /></el-icon>
            </span>
        </div>
    </div>
</template>
<script>
export default {
    name: "ScList",
    emits: ["change", "editItem", "removeItem"],
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
        hideDo: { type: Boolean, default: false },
        hideEdit: { type: Boolean, default: false },
        hideRemove: { type: Boolean, default: false },
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
        },
        itemEdit(item, index) {
            this.$emit('editItem', item, index);
        },
        itemRemove(item, index) {
            this.$emit('removeItem', item, index);
        }
    }
}
</script>
  
<style scoped>
.sc-list {}

.sc-list .active {
    background-color: lightblue;
}

.sc-list-item {
    display: flex;
    flex: 1;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    height: 100%;
    padding: 10px;
    border-bottom: dashed 1px #ccc;
}

.sc-list-item .opt {
    display: none;
}

.sc-list-item .opt i {
    margin-left: 5px;
    color: #999;
}

.sc-list-item .opt i:hover {
    color: #333;
}

.sc-list-item:hover {
    background-color: azure;
}

.sc-list-item:hover .opt {
    display: inline-block;
}
</style>