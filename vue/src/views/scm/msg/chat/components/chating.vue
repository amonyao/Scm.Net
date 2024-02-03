
<template>
    <el-container>
        <el-header>
            <div class="left-panel">
                <el-avatar @click="detailHandle" :size="32" :src="getAvatar()"></el-avatar>
                <div class="name">{{ chat.namec }}</div>
            </div>
        </el-header>
        <el-main>
            <el-scrollbar class="message-group-box" ref="refScrollbar" @scroll="scrollHandle">
                <div ref="refInner" class="list">
                    <el-icon v-show="loading">
                    </el-icon>
                    <span class="tips" v-show="!loading && list.length >= page.total">暂无更多</span>
                    <message v-show="!loading" :user="user" v-for="(item, index) in list" :key="index" :data="item"
                        class="margin-20-n" />
                </div>
            </el-scrollbar>
        </el-main>
        <el-footer style="height: auto;">
            <div class="message-input-container" :class="{ 'highlight': focus }">
                <div class="tool-box">
                    <el-popover placement="top" :width="400" trigger="click">
                        <template #reference>
                            <sc-icon class="icon" name="sc-smile-line" title="发送Emoji" />
                        </template>
                        <el-scrollbar class="emoji-scrollbar">
                            <div class="emoji-group">
                                <div class="emoji" v-for="(emoji, index) in emojis" :key="index" @click="setEmoji(emoji)">
                                    {{ emoji }}
                                </div>
                            </div>
                        </el-scrollbar>
                    </el-popover>
                    <el-upload class="upload" :action="uploadImage()" :show-file-list="false"
                        :on-success="uploadSuccessHandle" title="上传图像">
                        <sc-icon class="icon" name="sc-file-image-line" />
                    </el-upload>

                    <el-upload class="upload" :action="uploadFile()" :show-file-list="false"
                        :on-success="uploadSuccessHandle" title="上传文件">
                        <sc-icon class="icon" name="sc-file-line" />
                    </el-upload>
                </div>
                <div class="textarea">
                    <el-input v-model="content" type="textarea" resize="none" rows="4" @focus="inputFocus" @blur="inputBlur"
                        @keydown="listener" />
                    <el-tooltip effect="dark" placement="top-end" content="按Enter发送消息，Shift+Enter换行">
                        <el-button class="send">
                            <sc-icon name="sc-send-plane-line" @click="sendMsg" />
                        </el-button>
                    </el-tooltip>
                </div>
            </div>
        </el-footer>
    </el-container>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        message: defineAsyncComponent(() => import("./message")),
    },
    props: {
        user: { type: Object, default: null }
    },
    data() {
        return {
            loading: false,
            list: [],
            page: {
                total: 100
            },
            content: '',
            focus: false,
            emojis: '😃 😄 😁 😆 😅 🤣 😂 🙂 🙃 😉 😊 😇 😍 🤩 😘 😗 ☺️ 😚 😙 😋 😛 😜 🤪 😝 😝 🤗 🤭 🤫 🤔 🤐 🤨 😐 😑 😶 😏 😒 🙄 😬 🤥 😌 😔 😪 🤤 😴 😷 🤒 🤕 🤢 🤮 🤧 😵 🤯 🤠 😎 🤓 🧐 😕 😟 🙁 ☹️ 😮 😯 😲 😳 😦 😧 😨 😰 😥 😢 😭 😱 😖 😣 😞 😓 😩 😫 😤 😡 😠 🤬 😈 👿 💀 ☠️ 🤡 👹 👺 👻 👽 🙈 🙉 🙊 💋 💌 💘 💝 💖 💗 💓 💞 💕 💔 ❤️ 🧡 💛 💚 💙 💜 🖤 💬 🤳 👃 👅 👄 👶 🧒 👋 🤚 🖐️ ✋ 🖖 👌 ✌️ 🤞 🤟 🤘 🤙 👈 👉 👆 🖕 👇 ☝️ 👍 ⬆️ ➡️ ⬇️ ⬅️ ↩️ ↪️ ⤴️ ⤵️ 🔃 🔄 🔙 🔚 🔛 🔜 🔝 🔀 🔁 🔂 ▶️ ⏩ ⏭️ ⏯️ ◀️ ⏪ ⏮️ 🔼 ⏫ 🔽 ⏬'.split(' '),
            detailVisible: false,
            chat: {},
        }
    },
    methods: {
        setChat(chat) {
            this.chat = chat;
            this.listChats();
        },
        detailHandle() {

        },

        scrollHandle() {

        },
        getEmoji() {

        },
        setEmoji(emoji) {
            this.content += emoji;
        },
        async sendMsg() {
            var res = await this.$API.scmmsgchatmessage.chat.post({ id: this.chat.id, types: 1, content: this.content });
            if (!res || res.code != 200) {
                return;
            }

            this.list.push(res.data);
            this.content = '';
        },
        uploadImage() {

        },
        uploadFile() {

        },
        listener() {

        },
        uploadSuccessHandle() {

        },
        inputFocus() {
            this.focus = !this.focus;
        },
        inputBlur() {
            this.focus = !this.focus;
        },
        async listChats() {
            var res = await this.$API.scmmsgchatmessage.list_detail.get({ id: this.chat.id });
            if (!res || res.code != 200) {
                return;
            }

            this.list = res.data;
        },
        getAvatar() {
            var image = this.$SCM.get_avatar(this.chat);
            return this.$CONFIG.SERVER_URL + image;
        }
    }
}
</script>

<style lang="scss" scoped>
@import '@/assets/sass/_variable.scss';
$height: 50px;

.message-group-box {
    .list {
        width: 100%;
    }
}

.message-input-container {
    display: flex;
    flex-direction: column;

    .tool-box {
        .icon {
            font-size: 20px;
            margin-right: 5px;
            cursor: pointer;
        }

        .upload {
            display: inline;
        }
    }

    .textarea {
        position: relative;

        ::v-deep(.el-textarea__inner) {
            padding: 0;
            border: none;
            background: transparent;
        }

        .send {
            position: absolute;
            right: 5px;
            bottom: 5px;
            font-size: 24px;
            color: $activeColor;
            cursor: pointer;
        }

        ::-webkit-scrollbar {
            display: none;
        }
    }
}

.emoji-scrollbar {
    height: 200px;

    .emoji-group {
        display: flex;
        flex-wrap: wrap;

        .emoji {
            flex: 0 0 auto;
            width: 34px;
            font-size: 22px;
            cursor: pointer;
        }
    }
}
</style>