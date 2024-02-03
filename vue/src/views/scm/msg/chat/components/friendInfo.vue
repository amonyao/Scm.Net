<template>
    <div class="details">
        <div class="friend-details-box padding_t-20">
            <el-avatar :size="160" :src="user.avatar" />
            <p class="nickname">{{ user.nickname }}</p>
            <el-divider content-position="right" />
            <p class="remove" @click="removeHandle">删除好友</p>
        </div>
    </div>
</template>
<script>
export default {
    props: {
        user: { type: Object, default: null }
    },
    data() {
        return {

        }
    },
    methods: {
        removeHandle() {

        }
    }
}
</script>
<style lang="scss" scoped>
@import '@/assets/sass/_variable.scss';

.details {
    background-color: $lightColor-0;

    .friend-details-box {
        .nickname {
            font-size: 20px;
            color: $darkColor-8;
            font-weight: bold;
        }

        .remove {
            font-size: 14px;
            color: $warmColor;
            cursor: pointer;
        }
    }
}
</style>