<template>
    <div class="login-oauth">
        <el-button type="success" icon="sc-icon-wechat" circle @click="wechatLogin" title="微信登录"></el-button>
        <el-button type="success" icon="sc-icon-wechat" circle @click="wechatLogin" title="QQ登录"></el-button>
        <el-button type="success" icon="sc-icon-wechat" circle @click="wechatLogin" title="支付宝登录"></el-button>
        <el-button type="success" icon="sc-icon-wechat" circle @click="wechatLogin" title="其它登录"></el-button>
    </div>
    <el-dialog v-model="showWechatLogin" :title="$t('login.wechatLoginTitle')" :width="400" destroy-on-close>
        <div class="qrCodeLogin">
            <sc-qr-code class="qrCode" :text="WechatLoginCode" :size="200"></sc-qr-code>
            <p class="msg">
                {{ $tc("login.wechatLoginMsg", 1) }}<br />{{ $tc("login.wechatLoginMsg", 2) }}
            </p>
            <div class="qrCodeLogin-result" v-if="isWechatLoginResult">
                <el-result icon="success" :title="$tc('login.wechatLoginResult', 1)"
                    :sub-title="$tc('login.wechatLoginResult', 2)"></el-result>
            </div>
        </div>
    </el-dialog>
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
        wechatLogin() {
            this.showWechatLogin = true;
            this.WechatLoginCode = "Scm-823677237287236-" + new Date().getTime();
            this.isWechatLoginResult = false;
            setTimeout(() => {
                this.isWechatLoginResult = true;
            }, 3000);
        },
    }
}
</script>

<style>
.login-oauth {
    display: flex;
    justify-content: space-around;
}

.qrCodeLogin {
    text-align: center;
    position: relative;
    padding: 20px 0;
}

.qrCodeLogin img.qrCode {
    background: #fff;
    padding: 20px;
    border-radius: 10px;
}

.qrCodeLogin p.msg {
    margin-top: 15px;
}

.qrCodeLogin .qrCodeLogin-result {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    text-align: center;
    background: var(--el-mask-color);
}
</style>