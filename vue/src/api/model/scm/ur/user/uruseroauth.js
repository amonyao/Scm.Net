import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/uruseroauth/pages`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	list: {
		url: `${config.API_URL}/uruseroauth/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	view: {
		url: `${config.API_URL}/uruseroauth/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	edit: {
		url: `${config.API_URL}/uruseroauth/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	add: {
		url: `${config.API_URL}/uruseroauth`,
		name: "新增",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	update: {
		url: `${config.API_URL}/uruseroauth`,
		name: "更新",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	save: {
		url: `${config.API_URL}/uruseroauth`,
		name: "保存",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	status: {
		url: `${config.API_URL}/uruseroauth/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/uruseroauth`,
		name: "删除",
		delete: async function (data) {
			return await http.delete(this.url, data);
		}
	},
	dobind: {
		url: `${config.API_URL}/uruseroauth/dobind`,
		name: "绑定",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	unbind: {
		url: `${config.API_URL}/uruseroauth/unbind`,
		name: "解绑",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
}