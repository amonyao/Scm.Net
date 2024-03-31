<template>
    <div class="login-oauth">
        <div class="row">
            <div class="card" title="使用 华为 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('huawei')"></el-button>
                <div class="foot">华为</div>
            </div>
            <div class="card" title="使用 小米 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('mi')"></el-button>
                <div class="foot">小米</div>
            </div>
            <div class="card" title="使用 阿里云 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('aliyun')"></el-button>
                <div class="foot">阿里云</div>
            </div>
            <div class="card" title="使用 QQ 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('qq')"></el-button>
                <div class="foot">QQ</div>
            </div>
        </div>
        <div class="row">
            <div class="card" title="使用 Gitee 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('gitee')"></el-button>
                <div class="foot">Gitee</div>
            </div>
            <div class="card" title="使用 Github 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('github')"></el-button>
                <div class="foot">Github</div>
            </div>
            <div class="card" title="使用 Gitlab 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('gitlab')"></el-button>
                <div class="foot">Gitlab</div>
            </div>
            <div class="card" title="使用 OSChina 登录">
                <el-button type="success" icon="el-icon-link" circle @click="oauthLogin('oschina')"></el-button>
                <div class="foot">OSChina</div>
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
            window.location.href = 'https://sso.c-scm.net/oauth/login/' + type;
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
</style>