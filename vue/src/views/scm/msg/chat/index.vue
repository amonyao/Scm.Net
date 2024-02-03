<template>
    <el-container>
        <el-aside>
            <div class="sidebar">
                <div class="bar-left">
                    <avatar :user="user" @sendMe="sendMe" />
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
                <el-container>
                    <el-header>
                        <el-input class="search-input margin_r-10" prefix-icon="el-icon-search" placeholder="请输入名称或ID搜索"
                            v-model="keywords" />
                        <sc-icon name="sc-plus" @click="addHandle" />
                        <sc-icon name="sc-minus" @click="refresh" />
                    </el-header>
                    <el-main class="nopadding">
                        <transition name="el-fade-in-linear" mode="out-in">
                            <keep-alive>
                                <component :is="activeView" :key="active" :keywords="keywords" @chatMsg="chatMsg" />
                            </keep-alive>
                        </transition>
                    </el-main>
                    <!-- <dialog-add-friend ref="refAddFriend" v-if="friendVisible" /> -->
                </el-container>
            </div>
        </el-aside>
        <el-main class="nopadding">
            <div class="content">
                <transition name="el-fade-in-linear" mode="out-in">
                    <chating ref="message" :user="user" :detailVisible="detailVisible" class="content-message"
                        @on-detail="detailHandle" />
                </transition>
                <friend-info :user="user" class="content-details width-220" v-if="user.conversationId && detailVisible" />
            </div>
        </el-main>
    </el-container>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        avatar: defineAsyncComponent(() => import("./components/avatar")),
        chatList: defineAsyncComponent(() => import("./components/chatList")),
        friendList: defineAsyncComponent(() => import("./components/friendList")),
        friendInfo: defineAsyncComponent(() => import("./components/friendInfo")),
        chating: defineAsyncComponent(() => import("./components/chating")),
    },
    data() {
        return {
            user: {
                id: '0',
                account: "user@c-scm.net",
                namec: "user",
                avatar: "",
                sex: "男",
                summary: "",
                role_list: [],
                position_list: [],
            },
            group: { conversationId: '11' },
            detailVisible: false,
            icons: ['sc-wechat-line', 'sc-user-line', 'sc-bell-line'],
            active: 0,
            activePages: ['chatList', 'friendList', 'noticeList'],
            activeView: 'chatList',
            unreadCount: 0,
            applyCount: 0,
            keywords: '',
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            const res = await this.$API.login.user.get();
            this.user = res.data;
            this.user.conversationId = "111";
        },
        clickHandle(index) {
            this.active = index;
            this.activeView = this.activePages[index];
        },
        detailHandle() {
            this.detailVisible = !this.detailVisible;
        },
        async sendMe() {
            var res = await this.$API.scmmsgchatmessage.add.post({ types: 1, users: [this.user.id] });
            if (!res || res.code != 200) {
                return;
            }

            var chat = res.data;
            res = await this.$API.scmmsgchatmessage.list_detail.get({ id: chat.id });
            if (!res || res.code != 200) {
                return;
            }

            this.group = this.user;
            this.$refs.message.setGroup(this.group);
        },
        async sendMsg() {
        },
        async chatMsg(chat) {
            this.group = chat;
            this.$refs.message.setChat(this.group);
        },
        addHandle() {

        },
        refresh() {

        },
    }
}
</script>
<style lang="scss" scoped>
@import '@/assets/sass/_variable.scss';
$height: 70px;

.sidebar {
    background-color: #363e47;
    display: flex;
    width: 100%;
    height: 100%;

    .bar-left {
        background-color: #303841;
        width: 80px;

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
}

.content {
    display: flex;
    background-color: $lightColor-2;
    width: 100%;
    height: 100%;

    &-message {
        flex: 1
    }
}
</style>