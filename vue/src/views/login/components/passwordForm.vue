<template>
	<el-form ref="loginForm" :model="form" :rules="rules" label-width="0" size="large">
		<el-form-item prop="user">
			<el-input v-model="form.user" prefix-icon="el-icon-user" clearable
				:placeholder="$t('login.userPlaceholder')">
			</el-input>
		</el-form-item>
		<el-form-item prop="pass">
			<el-input v-model="form.pass" prefix-icon="el-icon-lock" clearable show-password
				:placeholder="$t('login.PWPlaceholder')"></el-input>
		</el-form-item>
		<el-form-item prop="code">
			<el-input v-model.trim="form.code" :placeholder="$t('login.userCode')" clearable
				prefix-icon="el-icon-set-up">
			</el-input>
			<el-image class="login-code" :src="codeUrl" @click="changeCode" />
		</el-form-item>
		<el-form-item style="margin-bottom: 10px">
			<el-col :span="12">
				<el-checkbox :label="$t('login.rememberMe')" v-model="form.autologin"></el-checkbox>
			</el-col>
			<el-col :span="12" class="login-forgot">
				<router-link to="/reset_password">{{ $t("login.forgetPassword") }}？</router-link>
			</el-col>
		</el-form-item>
		<el-form-item>
			<el-button type="primary" style="width: 100%" :loading="islogin" round @click="login">
				{{ $t("login.signIn") }}
			</el-button>
		</el-form-item>
		<div class="login-reg">
			{{ $t("login.noAccount") }}
			<router-link to="/user_register">{{ $t("login.createAccount") }}</router-link>
		</div>
	</el-form>
</template>

<script>
export default {
	data() {
		return {
			codeUrl: "img/loading.gif",
			form: {
				type: 10,
				user: this.$CONFIG.DEF_USER,
				pass: this.$CONFIG.DEF_PASS,
				code: undefined,
				codeKey: "",
				autologin: false,
			},
			rules: {
				user: [{ required: true, trigger: "blur", message: this.$t("login.userError") }],
				pass: [{ required: true, trigger: "blur", message: this.$t("login.PWError") }],
				code: [{ required: true, trigger: "blur", message: "验证码不能空" }],
			},
			islogin: false,
		};
	},
	mounted() {
		this.form.codeKey = this.$TOOL.uuid();
		this.codeUrl = this.$CONFIG.API_URL + "/captcha/cha/" + this.form.codeKey;
	},
	methods: {
		async login() {
			var validate = await this.$refs.loginForm.validate().catch(() => { });
			if (!validate) {
				return false;
			}

			this.islogin = true;
			this.checkAuth();
			this.islogin = false;
		},
		async checkAuth() {
			var pass = this.$CRYPTO.SHA(this.form.pass);
			var time = new Date().getTime();
			pass = this.$CRYPTO.SHA(pass + '@' + time);
			//pass = this.$SCM.encode_pass(pass);

			const data = {
				type: this.form.type,
				user: this.form.user,
				pass: pass,
				time: time,
				code: this.form.code,
				key: this.form.codeKey,
			};
			var userRes = await this.$API.login.token.post(data);
			if (userRes.code != 200) {
				this.$message.warning(userRes.message);
				return false;
			}
			var userData = userRes.data;
			if (userData.code != 0) {
				this.$message.warning(userData.message);
				return false;
			}
			this.$TOOL.data.set("TOKEN", userData.accessToken);
			this.$TOOL.data.set("USER_INFO", userData.userInfo);
			this.$TOOL.data.set("USER_THEME", userData.userTheme);

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
		changeCode() {
			this.codeUrl = this.$CONFIG.API_URL + "/captcha/cha/" + this.form.codeKey + `?timestamp=${new Date().getTime()}`;
		},
		async loadCfg() {
			var cfgRes = await this.$API.syscfgconfig.list.get({ 'types': 10 });
			if (!cfgRes || cfgRes.code != 200) {
				return;
			}
			var data = cfgRes.data;
			if (!data) {
				return;
			}

			data.forEach((item) => {
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
	},
};
</script>

<style scoped>
.login-code {
	position: absolute;
	top: 5px;
	right: 5px;
	cursor: pointer;
	border-radius: 5px;
	height: 30px;
}
</style>
