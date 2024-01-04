<template>
    <el-header style="height: auto;">
        <div class="sc_search">
            <div class="sc-search_params" v-show="isExpand">
                <el-form label-width="100px" :inline="true">
                    <slot name="search"></slot>
                    <el-form-item label="搜索内容">
                        <el-input v-model="key" clearable placeholder="关键字" />
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="doSearch">
                            <sc-icon icon="sc-search" />查询
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <div class="sc-search_filter">
                <div class="left-panel">
                    <el-space :size="15">
                        <slot name="filter">
                        </slot>
                    </el-space>
                </div>
                <div class="right-panel">
                    <div class="right-panel-search" v-if="!isExpand">
                        <el-input v-model="key" clearable placeholder="关键字" />
                        <el-button type="primary" @click="doSearch">
                            <sc-icon icon="sc-search" />
                        </el-button>
                        <el-button @click="doExpand" v-if="expandVisible">
                            <sc-icon icon="sc-arrow-up-2-fill" />展开
                        </el-button>
                    </div>
                    <div class="right-panel-search" v-else>
                        <el-button @click="doExpand">
                            <sc-icon icon="sc-arrow-down-2-fill" />合并
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
        expandVisible: { type: Boolean, default: false },
        searchVisible: { type: Boolean, default: false },
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
    border-bottom: 1px dashed #999;
    margin-bottom: 15px;
}

.sc-search_filter {
    display: flex;
    justify-content: space-between;
}
</style>