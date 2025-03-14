import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/uruser/pages`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	list: {
		url: `${config.API_URL}/uruser/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	add: {
		url: `${config.API_URL}/uruser`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	edit: {
		url: `${config.API_URL}/uruser/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	view: {
		url: `${config.API_URL}/uruser/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	option: {
		url: `${config.API_URL}/uruser/option`,
		name: "列表",
		get: async function (data) {
			return await http.get(this.url, data);
		},
	},
	update: {
		url: `${config.API_URL}/uruser`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	status: {
		url: `${config.API_URL}/uruser/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	delete: {
		url: `${config.API_URL}/uruser`,
		name: "删除",
		delete: async function (params) {
			return await http.delete(this.url + "?ids=" + params);
		},
	},
	simple: {
		url: `${config.API_URL}/uruser/simple`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	passreset: {
		url: `${config.API_URL}/uruser/passreset`,
		name: "重置密码",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	exportAll: {
		url: `${config.API_URL}/uruser/exportAll`,
		name: "修改密码",
		get: async function () {
			return await http.get(this.url);
		},
	},
	userData: {
		url: `${config.API_URL}/uruser/userData`,
		name: "修改数据权限",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
};
