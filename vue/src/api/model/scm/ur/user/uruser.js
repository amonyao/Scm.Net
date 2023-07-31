import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/uruser/pages`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	list: {
		url: `${config.API_URL}/uruser/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	add: {
		url: `${config.API_URL}/uruser`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	model: {
		url: `${config.API_URL}/uruser/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	update: {
		url: `${config.API_URL}/uruser`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	status: {
		url: `${config.API_URL}/uruser/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/uruser`,
		name: "删除",
		delete: async function (params) {
			return await http.delete(this.url + '?ids=' + params);
		}
	},
	simple: {
		url: `${config.API_URL}/uruser/simple`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	basic: {
		url: `${config.API_URL}/uruser/basic`,
		name: "修改登录人基本信息",
		put: async function (data = {}) {
			return await http.put(this.url, data);
		}
	},
	passreset: {
		url: `${config.API_URL}/uruser/passreset`,
		name: "重置密码",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	setpwd: {
		url: `${config.API_URL}/uruser/settingpwd`,
		name: "修改密码",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	exportAll: {
		url: `${config.API_URL}/uruser/exportAll`,
		name: "修改密码",
		get: async function () {
			return await http.get(this.url);
		}
	}
}
