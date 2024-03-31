<template>
    <el-container>
        <el-main>
            <div style="width:800px; margin: 0 auto;">
                <sc-list :data="oauth_list" :hide-do="true">
                    <template #item="{ item }">
                        <div class="oauth-item">
                            <div class="thumb">
                                <el-avatar :size="50" />
                            </div>
                            <div class="info">
                                <div class="label">{{ item.name }}</div>
                                <div class="value">{{ this.$TOOL.dateTimeFormat(item.create_time) }}</div>
                            </div>
                            <div class="opt">
                                <el-button type="danger" @click="del(item.id)">解绑</el-button>
                            </div>
                        </div>
                    </template>
                </sc-list>
                <div style="margin-top: 20px;">
                    <el-button type="primary" style="width: 100%;" @click="add()">添加</el-button>
                </div>
            </div>
        </el-main>
    </el-container>
</template>
<script>
import { useRoute } from "vue-router"

export default {
    data() {
        return {
            oauth_list: []
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            var route = useRoute();
            var key = route.query.key;
            var reg = /^[0-9a-zA-Z]{32}$/
            if (!reg.test(key)) {
                this.load();
                return;
            }

            this.doBind(key);
        },
        async doBind(key) {
            var res = await this.$API.uruseroauth.dobind.post({ 'key': key });
            if (res.code != 200) {
                this.$message.warning(res.message);
                return false;
            }

            var data = res.data;
            if (data.code != 0) {
                this.$message.warning(data.message);
            }
            else {
                this.$message.success('联合登录绑定成功！');
            }

            this.load();
        },
        async unBind(id) {
            var res = await this.$API.uruseroauth.unbind.post({ 'id': id });
            if (res.code != 200) {
                this.$message.warning(res.message);
                return false;
            }

            var data = res.data;
            if (data.code != 0) {
                this.$message.warning(data.message);
            }
            else {
                this.$message.success('联合登录解绑成功！');
            }

            this.load();
        },
        async load() {
            var res = await this.$API.uruseroauth.list.get();
            console.log(res);
            if (!res || res.code != 200) {
                return;
            }

            this.oauth_list = res.data;
            console.log('oauth:' + this.oauth_list.length);
        },
        add() {
            window.location.href = 'http://sso.c-scm.net/oauth';
        },
        del(id) {
            this.$confirm(
                `确定要操作解除绑定吗，解除后将无法正常使用三方登录？`,
                "提示",
                {
                    type: "warning",
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                }
            )
                .then(() => {
                    this.unBind(id);
                })
                .catch(() => { });
        }
    }
}
</script>

<style type="scss" scoped>
.oauth-item {
    display: flex;
    width: 100%;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding: 10px;

    .thumb {
        width: 50px;
        height: 50px;
        padding: 1px;
    }

    .info {
        flex-basis: 100%;

        margin: 0 20px;

        .label {
            font-weight: bold;
        }

        .value {
            color: gray;
            margin-top: 5px;
        }

    }

    .opt {}
}
</style>
