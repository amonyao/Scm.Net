
<template>
    <div class="bar-left">
        <avatar />
        <el-scrollbar>
            <ul>
                <li class="active-line" :style="{ top: active * 70 + 'px' }" />
                <li v-for="(icon, index) in icons" :key="index" @click="clickHandle(index)"
                    :class="active === index ? 'active' : ''">
                    <span class="unread" v-if="(index === 0 && unreadCount) || (index === 3 && applyCount)">
                        {{ index === 0 ? unreadCount : applyCount }}
                    </span>
                    <sc-icon :name="icon" />
                </li>
                <li @click="exitHandle">
                    <span class="iconfont icon-exit" />
                </li>
            </ul>
        </el-scrollbar>
    </div>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        avatar: defineAsyncComponent(() => import("./avatar")),
    },
    data() {
        return {
            icons: ['sc-wechat-line', 'sc-user-line', 'sc-bell-line'],
            active: 0,
            unreadCount: 0,
            applyCount: 0,
        }
    },
    methods: {
        clickHandle(index) {
            this.active = index;
        },
        exitHandle() {

        }
    }
}
</script>

<style lang="scss" scoped>
$height: 70px;

.bar-left {
    background-color: #303841;

    ul {
        position: relative;

        li {
            position: relative;
            z-index: 1;
            height: $height;
            line-height: $height;
            width: 100%;
            color: #a5b5c1;
            cursor: pointer;
            text-align: center;
            font-size: 32px;

            .unread {
                position: absolute;
                top: 10px;
                left: 10px;
                padding: 0px 6px;
                font-size: 12px;
                line-height: 18px;
                color: white;
                border-radius: 10px;
                background-color: red;
            }

            span {
                font-size: 30px;
            }
        }

        .active-line {
            position: absolute;
            z-index: 0;
            width: 100%;
            border-left: 4px solid #5cadff;
            transition: .3s;
            background-color: #363e47;
        }

        .active {
            color: #ffffff;
        }
    }
}
</style>