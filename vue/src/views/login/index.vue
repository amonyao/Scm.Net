<template>
	<div :style="theme.page" class="login_page">
		<div class="mask" :style="theme.mask"></div>
		<el-container class="main">
			<el-header>
				<div class="common-header-left">
					<div class="common-header-logo">
						<img :alt="$CONFIG.APP_NAME" src="img/logo.png">
						<label>{{ $CONFIG.APP_NAME }}</label>
					</div>
					<div class="common-header-title">{{ $t('login.signInTitle') }}</div>
				</div>
				<div class="common-header-right">
					<el-button :icon="config.dark ? 'el-icon-sunny' : 'el-icon-moon'" circle type="info" @click="configDark"
						:title="$t('login.themeTitle')">
					</el-button>
					<el-dropdown trigger="click" placement="bottom-end" @command="configLang"
						:title="$t('login.langTitle')">
						<el-button circle>
							<sc-icon name="sc-translate" />
						</el-button>
						<template #dropdown>
							<el-dropdown-menu>
								<el-dropdown-item v-for="item in lang" :key="item.value" :command="item"
									:class="{ selected: config.lang == item.value }">{{ item.name }}</el-dropdown-item>
							</el-dropdown-menu>
						</template>
					</el-dropdown>
				</div>
			</el-header>
			<el-main>
				<div class="login_card">
					<el-card class="login_main">
						<el-container>
							<el-header class="login-header">
								<div class="logo">
									<label>{{ $CONFIG.APP_NAME }}</label>
								</div>
							</el-header>
							<el-container>
								<el-aside class="login_aside">
									<div class="login_note">
										<div class="body">
											<div class="login_note__content">
												<div v-html="info.content"></div>
											</div>
										</div>
									</div>
								</el-aside>
								<el-main>
									<div class="login_user">
										<div class="body">
											<el-tabs>
												<el-tab-pane :label="$t('login.accountLogin')" lazy>
													<password-form></password-form>
												</el-tab-pane>
												<el-tab-pane :label="$t('login.phoneLogin')" lazy>
													<phone-form></phone-form>
												</el-tab-pane>
												<el-tab-pane :label="$t('login.emailLogin')" lazy>
													<email-form></email-form>
												</el-tab-pane>
												<el-tab-pane :label="$t('login.oauthLogin')" lazy>
													<oauth-form></oauth-form>
												</el-tab-pane>
											</el-tabs>
										</div>
									</div>
								</el-main>
							</el-container>
						</el-container>
					</el-card>
				</div>
			</el-main>
		</el-container>
	</div>
	<div class="login_foot">
		© {{ $CONFIG.APP_NAME }} {{ $CONFIG.APP_VER }} BUILD {{ $CONFIG.APP_BUILD }}
		<a href="https://beian.miit.gov.cn/" target="_blank" title="备案信息查询" v-if="$CONFIG.BEIAN_MIIT">
			{{ $CONFIG.BEIAN_MIIT }}
		</a>
		<a href="https://beian.mps.gov.cn/" target="_blank" title="备案信息查询" v-if="$CONFIG.BEIAN_MPS">
			{{ $CONFIG.BEIAN_MPS }}
		</a>
	</div>
</template>

<script>
import passwordForm from "./components/passwordForm";
import phoneForm from "./components/phoneForm";
import oauthForm from "./components/oauthForm";
import emailForm from "./components/emailForm";

export default {
	name: 'login',
	components: {
		passwordForm,
		phoneForm,
		oauthForm,
		emailForm,
	},
	data() {
		return {
			config: {
				lang: this.$TOOL.data.get("APP_LANG") || this.$CONFIG.LANG,
				dark: this.$TOOL.data.get("APP_DARK") || false,
			},
			lang: [
				{ name: "简体中文", value: "zh-cn", },
				{ name: "English", value: "en", },
			],
			info: {},
			theme: {
				page: {
					"backgroundColor": "",
					"backgroundSize": "cover",
					"backgroundPosition": "center center",
					"backgroundRepeat": "no-repeat"
				},
				mask: {
					"backgroundColor": "rgba(0,0,0,0.5)"
				}
			}
		};
	},
	watch: {
		"config.dark"(val) {
			if (val) {
				document.documentElement.classList.add("dark");
				this.$TOOL.data.set("APP_DARK", val);
			} else {
				document.documentElement.classList.remove("dark");
				this.$TOOL.data.remove("APP_DARK");
			}
		},
		"config.lang"(val) {
			this.$i18n.locale = val;
			this.$TOOL.data.set("APP_LANG", val);
		},
	},
	created: function () {
		this.$TOOL.cookie.remove("TOKEN");
		this.$TOOL.data.remove("USER_INFO");
		this.$TOOL.data.remove("MENU");
		this.$TOOL.data.remove("PERMISSIONS");
		this.$TOOL.data.remove("grid");
		this.$TOOL.data.remove("ISLONINEND");
		this.$store.commit("clearViewTags");
		this.$store.commit("clearKeepLive");
		this.$store.commit("clearIframeList");
	},
	mounted() {
		this.loadInfo();
		this.loadTheme();
	},
	methods: {
		async loadInfo() {
			var res = await this.$API.devapp.model.get({ 'code': 'scm.web' });
			if (res == null || res.code != 200) {
				return;
			}

			this.info = res.data;
		},
		async loadTheme() {
			var res = await this.$API.login.dateTheme.get({});
			if (res == null || res.code != 200) {
				return;
			}

			this.theme = res.data;
		},
		configDark() {
			this.config.dark = this.config.dark ? false : true;
		},
		configLang(command) {
			this.config.lang = command.value;
		},
	},
};
</script>

<style scoped>
.login_page {
	width: 100%;
	height: 100%;
	position: relative;
}

.login_page .mask {
	position: absolute;
	top: 0px;
	left: 0px;
	right: 0px;
	bottom: 0px;
	z-index: 1;
}

.login_page .main {
	position: absolute;
	top: 0px;
	left: 0px;
	right: 0px;
	bottom: 0px;
	z-index: 2;
}

.login_card {
	height: 100%;
	display: flex;
	justify-content: center;
	align-content: center;
}

.login_main {
	width: 100%;
	max-width: 800px;
	margin: auto;
}

.login_aside {
	width: 50%;
	padding: 15px;
}

.login_note {
	/* padding: 10px; */
}

.login_note .body {
	border-radius: 10px;
	min-height: 390px;
}

.login_note__title {
	padding: 40px;
}

.login_note__title h2 {
	font-size: 40px;
}

.login_note__title h4 {
	font-size: 18px;
	margin-top: 10px;
	font-weight: normal;
}

.login_note__title p {
	font-size: 14px;
	margin-top: 10px;
	line-height: 1.8;
	color: rgba(255, 255, 255, 0.6);
}

.login_note__title div {
	margin-top: 10px;
	display: flex;
	align-items: center;
}

.login_note__title div span {
	margin-right: 15px;
}

.login_note__title div i {
	font-size: 40px;
}

.login_note__title div i.add {
	font-size: 20px;
	color: rgba(255, 255, 255, 0.6);
}

.login_note__content {
	/* color: #666; */
}

.login-header {
	border: 0px;
}

.login-header .logo {
	display: flex;
	align-items: center;
}

.login-header .logo img {
	width: 40px;
	height: 40px;
	vertical-align: bottom;
	margin-right: 10px;
}

.login-header .logo label {
	font-size: 26px;
	font-weight: bold;
}

.login_foot {
	left: 0px;
	right: 0px;
	bottom: 0px;
	padding: 10px;
	position: absolute;
	text-align: center;
	color: #fff;
	z-index: 3;
}

.login_foot a {
	color: #fff;
}

.login_user {}

.login_user .body {
	border-radius: 10px;
}

.login_user .el-divider {
	margin-top: 40px;
}

.login_user:deep(.el-tabs) .el-tabs__header {
	margin-bottom: 25px;
}

.login_user:deep(.el-tabs) .el-tabs__header .el-tabs__item {
	font-size: 14px;
}

.login_user:deep(.login-forgot) {
	text-align: right;
}

.login_user:deep(.login-forgot) a {
	color: var(--el-color-primary);
}

.login_user:deep(.login-forgot) a:hover {
	color: var(--el-color-primary-light-3);
}

.login_user:deep(.login-reg) {
	font-size: 14px;
	color: var(--el-text-color-primary);
}

.login_user:deep(.login-reg) a {
	color: var(--el-color-primary);
}

.login_user:deep(.login-reg) a:hover {
	color: var(--el-color-primary-light-3);
}

.login_user:deep(.login-msg-yzm) {
	display: flex;
	width: 100%;
}

.login_user:deep(.login-msg-yzm) .el-button {
	margin-left: 10px;
	--el-button-size: 42px;
}

@media (max-width: 1200px) {
	.login_user {
		width: 340px;
	}
}

@media (max-width: 1000px) {
	.login_main {
		display: block;
	}

	.login_main .login_config {
		position: static;
		padding: 20px 20px 0 20px;
		text-align: right;
	}

	.login_user {
		width: 100%;
	}

	.login_aside {
		display: none;
	}
}
</style>
