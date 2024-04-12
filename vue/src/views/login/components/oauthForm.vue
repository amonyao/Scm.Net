<template>
    <div class="login-oauth">
        <div class="row">
            <div class="card" title="使用 华为 登录" @click="oauthLogin('huawei')">
                <el-avatar :size="32" :src="logoRoot + 'huawei.png'" />
                <div class="foot">华为</div>
            </div>
            <div class="card" title="使用 小米 登录" @click="oauthLogin('mi')">
                <el-avatar :size="32" :src="logoRoot + 'mi.png'" />
                <div class="foot">小米</div>
            </div>
            <div class="card" title="使用 阿里云 登录" @click="oauthLogin('aliyun')">
                <el-avatar :size="32" :src="logoRoot + 'aliyun.png'" />
                <div class="foot">阿里云</div>
            </div>
            <div class="card" title="使用 QQ 登录" @click="oauthLogin('qq')">
                <el-avatar :size="32" :src="logoRoot + 'qq.png'" />
                <div class="foot">QQ</div>
            </div>
        </div>
        <div class="row">
            <div class="card" title="使用 Gitee 登录" @click="oauthLogin('gitee')">
                <el-avatar :size="32" :src="logoRoot + 'gitee.png'" />
                <div class="foot">Gitee</div>
            </div>
            <div class="card" title="使用 Github 登录" @click="oauthLogin('github')">
                <el-avatar :size="32" :src="logoRoot + 'github.png'" />
                <div class="foot">Github</div>
            </div>
            <div class="card" title="使用 Gitlab 登录" @click="oauthLogin('gitlab')">
                <el-avatar :size="32" :src="logoRoot + 'gitlab.png'" />
                <div class="foot">Gitlab</div>
            </div>
            <div class="card" title="使用 WPS 登录" @click="oauthLogin('wps')">
                <el-avatar :size="32" :src="logoRoot + 'wps.png'" />
                <div class="foot">WPS</div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            WechatLoginCode: "",
            showWechatLogin: false,
            isWechatLoginResult: false,
            form: {
                email: "",
                yzm: "",
            },
            rules: {
                email: [
                    { required: true, message: this.$t('login.emailError') }
                ],
                yzm: [
                    { required: true, message: this.$t('login.msgError') }
                ]
            },
            disabled: false,
            time: 0,
            islogin: false,
            logoRoot: 'http://sso.c-scm.net/logo/',
        }
    },
    mounted() {

    },
    methods: {
        async getYzm() {
            var validate = await this.$refs.loginForm.validateField("email").catch(() => { })
            if (!validate) { return false }

            this.$message.success(this.$t('login.msgSent'))
            this.disabled = true
            this.time = 60
            var t = setInterval(() => {
                this.time -= 1
                if (this.time < 1) {
                    clearInterval(t)
                    this.disabled = false
                    this.time = 0
                }
            }, 1000)
        },
        oauthLogin(type) {
            window.location.href = 'https://sso.c-scm.net/oauth/login/' + type + '?mode=login';
        },
    }
}
</script>

<style type="scss" scoped>
.login-oauth {
    display: flex;
    flex-direction: column;

    .row {
        display: flex;
        justify-content: space-around;
        text-align: center;
    }

    .card {
        width: 100px;
        height: 86px;
        padding: 10px;
        border-radius: 5px;
        cursor: pointer;
        border: 1px solid #fff;

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