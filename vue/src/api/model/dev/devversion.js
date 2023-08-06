import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/devversion/pages`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	list: {
		url: `${config.API_URL}/devversion/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	view: {
		url: `${config.API_URL}/devversion/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	edit: {
		url: `${config.API_URL}/devversion/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	add: {
		url: `${config.API_URL}/devversion`,
		name: "新增",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	update: {
		url: `${config.API_URL}/devversion`,
		name: "更新",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	save: {
		url: `${config.API_URL}/devversion`,
		name: "保存",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	status: {
		url: `${config.API_URL}/devversion/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/devversion`,
		name: "删除",
		delete: async function (data) {
			return await http.delete(this.url, data);
		}
	},
	ver: {
		url: `${config.API_URL}/devversion/ver`,
		name: "当前版本",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	current: {
		url: `${config.API_URL}/devversion/current/`,
		name: "修改版本",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
}