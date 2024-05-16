<template>
    <div class="login-oauth">
        <div class="card" v-for="item in osp_list" :key="item.code" :title="getTitle(item.name)"
            @click="oauthLogin(item.code)">
            <el-avatar :size="32" :src="getLogo(item.icon)" />
            <div class="foot">{{ item.name }}</div>
        </div>
    </div>
</template>
<script>
import config from "@/config";

export default {
    data() {
        return {
            osp_list: [],
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            var url = config.OIDC_OSP + config.OIDC_KEY;
            var res = await this.$API.login.listOsp.get(url);
            if (!res || res.code != 0) {
                return;
            }

            this.osp_list = res.data;
        },
        getTitle(name) {
            return `使用 ${name} 登录`;
        },
        getLogo(icon) {
            var url = config.OIDC_LOGO || '';
            if (!url.endsWith('/')) {
                url += '/';
            }
            return url + icon;
        },
        oauthLogin(osp) {
            var url = config.OIDC_AUTH || '';
            url = url.replace('{osp}', osp).replace('{key}', config.OIDC_KEY).replace('{state}', 'login');
            window.location.href = url;
        },
    }
}
</script>

<style type="scss" scoped>
.login-oauth {
    display: grid;
    grid-template-columns: auto auto auto auto;

    .card {
        padding: 10px;
        border-radius: 5px;
        cursor: pointer;
        border: 1px solid #fff;
        text-align: center;

        .foot {
            margin-top: 10px;
            border: 1px solid #f8f8f8;
            line-height: 20px;
            border-radius: 3px;
            background-color: #fafafa;
        }
    }

    .card:hover {
        border: 1px solid #ccc;
    }
}

.el-avatar {
    background-color: #fff;

    img {
        width: 24px;
        height: 24px;
    }
}
</style>