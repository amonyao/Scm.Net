<template>
    <div class="portrait-container">
        <el-popover placement="right" :width="200" trigger="click">
            <template #reference>
                <el-avatar class="portrait" :size="50" :src="user.avatar" />
            </template>
            <div class="info-popover">
                <span class="edit iconfont icon-set" @click="editHandle" />
                <el-avatar :size="70" :src="user.avatar" />
                <div class="nickname-box ellipse-1">
                    <el-tooltip effect="dark" placement="top" :content="user.namec">
                        <span>{{ user.namec }}</span>
                    </el-tooltip>
                </div>
            </div>
        </el-popover>
    </div>
    <!-- <dialog-edit-information ref="refDialog" v-if="visible" /> -->
</template>
<script>
export default {
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
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            const res = await this.$API.login.user.get();
            this.user = res.data;
        },
        editHandle() {

        }
    }
}
</script>

<style lang="scss" scoped>
.portrait-container {
    padding: 15px 0;
    text-align: center;

    .portrait {
        cursor: pointer;
    }
}

.info-popover {
    text-align: center;
    position: relative;

    .edit {
        position: absolute;
        right: 0;
        font-size: 12px;
        cursor: pointer;
        animation: rotateAgainst360 .3s ease-out 0s;

        &:hover {
            color: #5cadff;
            animation: rotateAlong360 .4s ease-out 0s;
        }
    }

    .nickname-box {
        font-size: 14px;
        color: #606266;
        max-width: 100px;
        margin: 0 auto;
    }
}
</style>