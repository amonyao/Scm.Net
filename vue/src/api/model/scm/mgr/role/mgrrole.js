import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/mgrrole/pages`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	list: {
		url: `${config.API_URL}/mgrrole/list`,
		name: "租户角色查询",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	add: {
		url: `${config.API_URL}/mgrrole`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	model: {
		url: `${config.API_URL}/mgrrole/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	update: {
		url: `${config.API_URL}/mgrrole`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	option: {
		url: `${config.API_URL}/mgrrole/option/`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	status: {
		url: `${config.API_URL}/mgrrole/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	delete: {
		url: `${config.API_URL}/mgrrole`,
		name: "删除",
		delete: async function (params) {
			return await http.delete(this.url + "?ids=" + params);
		},
	},
};
