<template>
    <div class="bar-right">
        <div class="header">
            <el-input class="search-input margin_r-10" prefix-icon="el-icon-search" placeholder="请输入名称或ID搜索"
                v-model="keywords" />
            <sc-icon name="sc-plus" @click="addHandle" />
            <sc-icon name="sc-minus" @click="refresh" />
        </div>
        <el-scrollbar class="body">
            <transition name="el-fade-in-linear" mode="out-in">
                <keep-alive>
                    <component :is="view" :key="active" :keywords="keywords" />
                </keep-alive>
            </transition>
        </el-scrollbar>
        <!-- <dialog-add-friend ref="refAddFriend" v-if="friendVisible" /> -->
    </div>
</template>

<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        chatList: defineAsyncComponent(() => import("./chatList")),
        userList: defineAsyncComponent(() => import("./userList")),
    },
    data() {
        return {
            view: 'chatList',
            active: 0,
            keywords: '',
        }
    },
    methods: {
        addHandle() {

        },
        refresh() {

        }
    }
}
</script>
<style lang="scss" scoped>
$height: 50px;

.bar-right {
    flex: 1;

    .header {
        height: $height;
        padding: 10px 10px 10px 20px;
        display: flex;
        border-bottom: 1px solid #303841;

        .search-input {
            flex: 1;
        }

        ::v-deep(.search-input input) {
            color: #303841;
            background-color: #303841;
            border-radius: 30px;
            border: none;

            &::-webkit-input-placeholder {
                color: #303841;
                font-size: 12px;
            }
        }

        .icon-btn {
            height: 30px;
            width: 30px;
            line-height: 30px;
            color: #303841;
            border-radius: 50%;
            background-color: #303841;
            cursor: pointer;
            animation: rotateAgainst360 .3s ease-out 0s;

            &:hover {
                color: #5cadff;
                animation: rotateAlong360 .4s ease-out 0s;
            }
        }
    }

    .body {
        height: calc(100% - 50px);

        ::v-deep(.el-scrollbar__bar.is-vertical) {
            display: none !important;
        }
    }
}</style>