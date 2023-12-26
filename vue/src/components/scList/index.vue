<template>
    <div class="sc-list">
        <slot name="header">
            <div class="sc-list_header" v-if="header">{{ header }}</div>
        </slot>
        <el-empty :description="emptyText" :image-size="100" v-if="isEmpty()"></el-empty>
        <div class="sc-list_body" :style="{ padding: this.padding + 'px' }" v-else>
            <div v-for="(item, index) in data" :key="index" @click="itemClick(item, index)" class="sc-list-item"
                :class="{ active: canSelected && selectedIndex == index }" :style="{ width: 100 / columns + '%' }">
                <slot name="item" :item="item" :index="index">
                    <div class="sc-list-item_label">
                        <sc-icon :icon="icon"></sc-icon>
                        {{ item.label }}
                    </div>
                    <div class="sc-list-item_value">
                        <span v-if="item.value">{{ item.value }}</span>
                        <el-tag round v-if="item.count">{{ item.count }}</el-tag>
                    </div>
                </slot>
                <span class="opt" v-if="showOpt">
                    <el-icon @click.stop="itemEdit(item, index)" v-if="showEdit" title="编辑">
                        <el-icon-edit />
                    </el-icon>
                    <el-icon @click.stop="itemRemove(item, index)" v-if="showDrop" title="删除">
                        <el-icon-delete />
                    </el-icon>
                </span>
            </div>
        </div>
        <slot name="footer">
            <div class="sc-list_footer" v-if="footer">{{ footer }}</div>
        </slot>
    </div>
</template>
<script>
export default {
    name: "ScList",
    emits: ["change", "editItem", "dropItem"],
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
        icon: { type: String, default: '' },
        data: { type: Array, default: function () { return []; } },
        width: { type: String, default: "" },
        canSelected: { type: Boolean, default: true },
        showOpt: { type: Boolean, default: false },
        showEdit: { type: Boolean, default: false },
        showDrop: { type: Boolean, default: false },
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
            this.$emit('dropItem', item, index);
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

.sc-list .sc-list-item .sc-list-item_label {}

.sc-list .sc-list-item .sc-list-item_value {
    color: gray;
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
