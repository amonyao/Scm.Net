import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/sysconfig/pages`,
		name: "分页",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	list: {
		url: `${config.API_URL}/sysconfig/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	add: {
		url: `${config.API_URL}/sysconfig`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	model: {
		url: `${config.API_URL}/sysconfig/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	update: {
		url: `${config.API_URL}/sysconfig`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	save: {
		url: `${config.API_URL}/sysconfig/save`,
		name: "更新配置",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	status: {
		url: `${config.API_URL}/sysconfig/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/sysconfig`,
		name: "删除",
		delete: async function (params) {
			return await http.delete(this.url + "?ids=" + params);
		},
	},
};
