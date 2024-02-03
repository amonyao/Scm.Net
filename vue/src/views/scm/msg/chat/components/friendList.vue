<template>
    <sc-list :data="friendList">
    </sc-list>
</template>
<script>
export default {
    data() {
        return {
            apiObj: '',
            friendList: []
        }
    },
    props: {
        keywords: { type: [Number, String, Boolean], required: false }
    },
    mounted() {
        this.listFriends();
    },
    methods: {
        async listFriends() {
            var res = await this.$API.scmmsgchatfriend.list.get();
            if (!res || res.code != 200) {
                return;
            }

            this.friendList = res.data;
        }
    }
}
</script>