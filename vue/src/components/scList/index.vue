<template>
    <div class="sc-list">
        <slot name="header">
            <div class="sc-list_header" v-if="header">
                {{ header }}
            </div>
        </slot>
        <el-empty :description="emptyText" :image-size="100" v-if="isEmpty()"></el-empty>
        <div class="sc-list_body" :style="{ padding: this.padding + 'px' }" v-else>
            <div v-for="(item, index) in data" @click="itemClick(item, index)" :key="index" class="sc-list-item"
                :style="{ width: 100 / columns + '%' }" :class="canSelected && selectedIndex == index ? 'active' : ''">
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
                    <el-icon @click.stop="itemRemove(item, index)" v-if="!hideRemove"
                        title="删除"><el-icon-delete /></el-icon>
                </span>
            </div>
        </div>
        <slot name="footer">
            <div class="sc-list_footer" v-if="footer">
                {{ footer }}
            </div>
        </slot>
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
        header: { type: String, default: '' },
        footer: { type: String, default: '' },
        data: { type: Array, default: function () { return []; } },
        result: { type: Object, default: () => { } },
        width: { type: String, default: "" },
        canSelected: { type: Boolean, default: true },
        hideDo: { type: Boolean, default: false },
        hideEdit: { type: Boolean, default: false },
        hideRemove: { type: Boolean, default: false },
        columns: { type: Number, default: 1 },
        padding: { type: Number, default: 0 },
        emptyText: { type: String, default: "" },
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
        },
        isEmpty() {
            return this.data == null || this.data.length < 1;
        }
    }
}
</script>
  
<style scoped>
.sc-list {
    width: 100%;
    background-color: var(--el-fill-color-blank);
    border: 1px solid var(--el-border-color-lighter);
    border-radius: var(--el-border-radius-base);
}

.sc-list .sc-list-item {
    display: flex;
    /* flex: 1; */
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    height: 100%;
    padding: 10px;
    background-color: var(--el-fill-color-blank);
}

.sc-list .active {
    background-color: var(--el-color-primary-light-9);
}

.sc-panel {
    margin: 15px;
}

.sc-list_header {
    border-bottom: 0;
    font-size: 17px;
    font-weight: bold;
    padding: 15px 20px;
}

.sc-list_body {
    overflow-y: auto;
}

.sc-list_footer {
    color: var(--el-text-color-primary);
    font-size: 16px;
    font-weight: 700;
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
    background-color: var(--el-fill-color-light);
}

.sc-list-item:hover .opt {
    display: inline-block;
}
</style>