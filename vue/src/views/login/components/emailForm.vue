<template>
	<el-form ref="loginForm" :model="form" :rules="rules" label-width="0" size="large">
		<el-form-item prop="email">
			<el-input v-model="form.email" prefix-icon="el-icon-message" clearable :placeholder="$t('login.emailPlaceholder')">
			</el-input>
		</el-form-item>
		<el-form-item prop="yzm"  style="margin-bottom: 35px;">
			<div class="login-msg-yzm">
				<el-input v-model="form.yzm" prefix-icon="el-icon-unlock" clearable :placeholder="$t('login.msgPlaceholder')"></el-input>
				<el-button @click="getYzm" :disabled="disabled">{{this.$t('login.msgGet')}}<span v-if="disabled"> ({{time}})</span></el-button>
			</div>
		</el-form-item>
		<el-form-item>
			<el-button type="primary" style="width: 100%;" :loading="islogin" round @click="login">{{ $t('login.signIn') }}</el-button>
		</el-form-item>
		<div class="login-reg">
			{{$t('login.noAccount')}} <router-link to="/user_register">{{$t('login.createAccount')}}</router-link>
		</div>
	</el-form>
</template>

<script>
	export default {
		data() {
			return {
				form: {
					email: "",
					yzm: "",
				},
				rules: {
					email: [
						{required: true, message: this.$t('login.emailError')}
					],
					yzm: [
						{required: true, message: this.$t('login.msgError')}
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
			async getYzm(){
				var validate = await this.$refs.loginForm.validateField("email").catch(()=>{})
				if(!validate){ return false }

				this.$message.success(this.$t('login.msgSent'))
				this.disabled = true
				this.time = 60
				var t = setInterval(() => {
					this.time -= 1
					if(this.time < 1){
						clearInterval(t)
						this.disabled = false
						this.time = 0
					}
				},1000)
			},
			async login(){
				var validate = await this.$refs.loginForm.validate().catch(()=>{})
				if(!validate){ return false }
			}
		}
	}
</script>

<style>

</style>
