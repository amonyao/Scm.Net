<template>
    <el-header style="height: auto;">
        <div class="sc_search">
            <div class="sc-search_params" v-show="isExpand">
                <slot name="search">
                    <el-form ref="formRef" label-width="100px" :model="param" :inline="true">
                        <el-form-item label="搜索内容">
                            <el-input v-model="key" clearable placeholder="关键字" />
                        </el-form-item>
                        <el-form-item>
                            <el-button type="primary" @click="doSearch">
                                <sc-icon name="sc-search" />查询
                            </el-button>
                        </el-form-item>
                    </el-form>
                </slot>
            </div>
            <div class="sc-search_filter">
                <div class="left-panel" v-if="filterVisible">
                    <slot name="filter">
                    </slot>
                </div>
                <div class="right-panel">
                    <div class="right-panel-search" v-if="!isExpand">
                        <el-input v-model="key" clearable placeholder="关键字" />
                        <el-button type="primary" @click="doSearch">
                            <sc-icon name="sc-search" />
                        </el-button>
                        <el-button @click="doExpand" v-if="expandVisible">
                            <sc-icon name="sc-arrow-up-2-fill" />展开
                        </el-button>
                    </div>
                    <div class="right-panel-search" v-else>
                        <el-button @click="doExpand">
                            <sc-icon name="sc-arrow-down-2-fill" />合并
                        </el-button>
                    </div>
                </div>
            </div>
        </div>
    </el-header>
</template>

<script>
export default {
    emits: ['search'],
    props: {
        expandVisible: { type: Boolean, default: true },
        searchVisible: { type: Boolean, default: true },
        filterVisible: { type: Boolean, default: true },
        syncSearch: { type: Boolean, default: false },
    },
    data() {
        return {
            param: {},
            isExpand: false,
            key: ''
        }
    },
    watch: {
        key(val) {
            if (this.syncSearch) {
                this.$emit('search', val);
            }
        },
    },
    methods: {
        doSearch() {
            this.$emit('search', this.key);
        },
        doExpand() {
            this.isExpand = !this.isExpand;
        }
    }
}
</script>

<style>
.sc_search {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    width: 100%;
}

.sc-search_params {
    border-bottom: 1px dashed var(--el-border-color-light);
    margin-bottom: 15px;
}

.sc-search_filter {
    display: flex;
    justify-content: space-between;
}
</style>