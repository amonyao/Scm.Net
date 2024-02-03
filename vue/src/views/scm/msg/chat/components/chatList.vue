<template>
    <sc-list :data="chatList" @change="change">
        <template #item="{ item }">
            <sc-summary :title="item.namec" :summary="item.update_time" :image="getAvatar(item)"></sc-summary>
        </template>
    </sc-list>
    <select-user hide-input v-model:selectOpen="isOpenUser" @onSelect="selectUserRes"></select-user>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        selectUser: defineAsyncComponent(() => import("@/components/scSelectUser"))
    },
    data() {
        return {
            apiObj: '',
            chatList: [],
            isOpenUser: false
        }
    },
    props: {
        keywords: { type: [Number, String, Boolean], required: false }
    },
    emits: ['chatMsg'],
    mounted() {
        this.listChats();
    },
    methods: {
        async listChats() {
            var res = await this.$API.scmmsgchatmessage.list_header.get();
            if (!res || res.code != 200) {
                return;
            }

            this.chatList = res.data;
        },
        change(group) {
            this.$emit('chatMsg', group);
        },
        getAvatar(item) {
            var image = this.$SCM.get_avatar(item);
            return this.$CONFIG.SERVER_URL + image;
        },
		openSelectUser() {
			if (this.isOpenUser) {
				this.isOpenUser = false;
			}
			this.isOpenUser = true;
		},
		selectUserRes(res) {
			this.isOpenUser = false;
			//this.erceipt = res;
		},
    }
}
</script>