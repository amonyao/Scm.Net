import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/mgrunit/pages`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	list: {
		url: `${config.API_URL}/mgrunit/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	view: {
		url: `${config.API_URL}/mgrunit/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	edit: {
		url: `${config.API_URL}/mgrunit/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	add: {
		url: `${config.API_URL}/mgrunit`,
		name: "新增",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	update: {
		url: `${config.API_URL}/mgrunit`,
		name: "更新",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	save: {
		url: `${config.API_URL}/mgrunit/save`,
		name: "保存",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	option: {
		url: `${config.API_URL}/mgrunit/option/0`,
		name: "列表",
		get: async function () {
			return await http.get(this.url);
		}
	},
	status: {
		url: `${config.API_URL}/mgrunit/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/mgrunit`,
		name: "删除",
		delete: async function (params) {
			return await http.delete(this.url + '?ids=' + params);
		}
	},
	resetData: {
		url: `${config.API_URL}/mgrunit/ResetData/`,
		name: "删除",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	clearData: {
		url: `${config.API_URL}/mgrunit/ClearData/`,
		name: "删除",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	}
}