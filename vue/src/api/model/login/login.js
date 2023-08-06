import config from "@/config"
import http from "@/utils/request"

export default {
	token: {
		url: `${config.API_URL}/operator/login`,
		name: "登录获取TOKEN",
		post: async function (data = {}) {
			return await http.post(this.url, data);
		}
	},
	user: {
		url: `${config.API_URL}/operator/userword`,
		name: "查询登录人信息",
		get: async function (data = {}) {
			return await http.get(this.url, data);
		}
	},
	signIn: {
		url: `${config.API_URL}/operator/signin`,
		name: "登录",
		post: async function (data = {}) {
			return await http.post(this.url, data);
		}
	},
	signon: {
		url: `${config.API_URL}/operator/signon`,
		name: "注册",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	dateTheme: {
		url: `${config.API_URL}/operator/datetheme`,
		name: "获取登录风格",
		get: async function (data) {
			return await http.get(this.url, data);
		}
	},
	userTheme: {
		url: `${config.API_URL}/operator/usertheme`,
		name: "获取登录风格",
		get: async function (data) {
			return await http.get(this.url, data);
		}
	},
	userBasic: {
		url: `${config.API_URL}/operator/userbasic`,
		name: "修改登录人基本信息",
		put: async function (data = {}) {
			return await http.put(this.url, data);
		}
	},
	userPass: {
		url: `${config.API_URL}/operator/userpass`,
		name: "修改密码",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
}
