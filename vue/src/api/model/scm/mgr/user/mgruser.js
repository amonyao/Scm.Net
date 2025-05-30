import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/mgruser/pages`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	add: {
		url: `${config.API_URL}/mgruser`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	model: {
		url: `${config.API_URL}/mgruser/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	edit: {
		url: `${config.API_URL}/mgruser/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	view: {
		url: `${config.API_URL}/mgruser/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	update: {
		url: `${config.API_URL}/mgruser`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	status: {
		url: `${config.API_URL}/mgruser/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	delete: {
		url: `${config.API_URL}/mgruser`,
		name: "删除",
		delete: async function (params) {
			return await http.delete(this.url + "?ids=" + params);
		},
	},
	basic: {
		url: `${config.API_URL}/mgruser/basic`,
		name: "修改登录人基本信息",
		put: async function (data = {}) {
			return await http.put(this.url, data);
		},
	},
	passreset: {
		url: `${config.API_URL}/mgruser/passreset`,
		name: "重置密码",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	setpwd: {
		url: `${config.API_URL}/mgruser/settingpwd`,
		name: "修改密码",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
};
