<template>
	<el-form ref="loginForm" :model="form" :rules="rules" label-width="0" size="large">
		<el-form-item prop="email">
			<el-input v-model="form.email" prefix-icon="el-icon-message" clearable
				:placeholder="$t('login.emailPlaceholder')">
			</el-input>
		</el-form-item>
		<el-form-item prop="code" style="margin-bottom: 35px;">
			<div class="login-msg-yzm">
				<el-input v-model="form.code" prefix-icon="el-icon-unlock" clearable
					:placeholder="$t('login.msgPlaceholder')"></el-input>
				<el-button @click="getYzm" :disabled="disabled">
					{{ this.$t('login.msgGet') }}<span v-if="disabled">({{ time }})</span>
				</el-button>
			</div>
		</el-form-item>
		<el-form-item>
			<el-button type="primary" style="width: 100%;" :loading="islogin" round @click="login">
				{{ $t('login.signIn') }}
			</el-button>
		</el-form-item>
		<div class="login-reg">
			{{ $t('login.noAccount') }} <router-link to="/user_register">{{ $t('login.createAccount') }}</router-link>
		</div>
	</el-form>
</template>

<script>
export default {
	data() {
		return {
			form: {
				type: 30,
				email: '',
				code: '',
				key: '',
			},
			rules: {
				email: [{ required: true, message: this.$t('login.emailError') }],
				code: [{ required: true, message: this.$t('login.msgError') }]
			},
			disabled: false,
			time: 0,
			islogin: false,
		}
	},
	mounted() {
		this.init();
	},
	methods: {
		init() {
			var key = this.$SCM.read_cache('scm_login_email', '');
			if (!key) {
				key = this.$TOOL.uuid();
			}
			this.form.key = key;
		},
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
			}, 1000);

			var data = {
				type: this.form.type,
				code: this.form.email,
				key: this.form.key,
			};
			var userRes = await this.$API.login.sendSms.post(data);
			if (userRes.code != 200) {
				this.$message.warning(userRes.message);
				return false;
			}
		},
		async login() {
			var validate = await this.$refs.loginForm.validate().catch(() => { })
			if (!validate) { return false }

			this.islogin = true;
			this.checkAuth();
			this.islogin = false;
		},
		async checkAuth() {
			var userRes = await this.$API.login.token.post(this.form);
			if (userRes.code != 200) {
				this.$message.warning(userRes.message);
				return false;
			}

			this.$SCM.save_cache('scm_login_pone', '');
			this.$TOOL.data.set("TOKEN", userRes.data.accessToken);
			this.$TOOL.data.set("USER_INFO", userRes.data.userInfo);
			this.$TOOL.data.set("USER_THEME", userRes.data.userTheme);

			//获取菜单
			var menuRes = await this.$API.login.authority.get();
			if (menuRes.code != 200) {
				this.$message.warning(menuRes.message);
				return false;
			}
			if (menuRes.data.length == 0) {
				this.$alert(
					"当前用户无任何菜单权限，请联系系统管理员",
					"无权限访问",
					{ type: "error", center: true }
				);
				return false;
			}
			var menuList = this.$SCM.recursive_menu(menuRes.data, '0');
			this.$TOOL.data.set("MENU", menuList);
			this.$TOOL.data.set("PERMISSIONS", []);

			this.loadCfg();

			let path = userRes.data.userInfo.unitId == 0 ? "/" : "/";//TODO:区分管理员与用户
			this.$router.replace({ path: path });
			this.$message.success("Login Success 登录成功");
		},
		async loadCfg() {
			var cfgRes = await this.$API.syscfgconfig.list.get({ 'types': 10 });
			cfgRes.data.forEach((item) => {
				if ("app_theme" == item.key) {
					if (item.value == "true") {
						document.documentElement.classList.add("dark")
					} else {
						document.documentElement.classList.remove("dark")
					}
					return;
				}
				if ("app_color" == item.key) {
					//this.config.colorPrimary = item.val;
					return;
				}
				if ("app_lang" == item.key) {
					//this.config.lang = item.value;
				}
			});
		}
	}
}
</script>

<style></style>
