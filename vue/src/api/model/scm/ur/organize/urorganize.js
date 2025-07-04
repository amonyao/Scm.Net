import config from "@/config";
import http from "@/utils/request";
export default {
	list: {
		url: `${config.API_URL}/urorganize/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	add: {
		url: `${config.API_URL}/urorganize`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	model: {
		url: `${config.API_URL}/urorganize/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	update: {
		url: `${config.API_URL}/urorganize`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	status: {
		url: `${config.API_URL}/urorganize/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	delete: {
		url: `${config.API_URL}/urorganize`,
		name: "删除",
		delete: async function (data) {
			return await http.delete(this.url, data);
		},
	},
	changeOwner: {
		url: `${config.API_URL}/urorganize/changeOwner`,
		name: "更新主管",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
};
