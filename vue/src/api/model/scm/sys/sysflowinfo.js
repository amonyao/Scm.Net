import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/sysflowinfo/page`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	view: {
		url: `${config.API_URL}/sysflowinfo/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	edit: {
		url: `${config.API_URL}/sysflowinfo/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	add: {
		url: `${config.API_URL}/sysflowinfo`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	model: {
		url: `${config.API_URL}/sysflowinfo/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	update: {
		url: `${config.API_URL}/sysflowinfo`,
		name: "修改",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/sysflowinfo`,
		name: "删除",
		delete: async function (data) {
			return await http.delete(this.url, data);
		}
	},
	readflow: {
		url: `${config.API_URL}/sysflowinfo/readflow`,
		name: "读取流程",
		get: async function (data) {
			return await http.get(this.url, data);
		}
	},
	saveflow: {
		url: `${config.API_URL}/sysflowinfo/saveflow`,
		name: "保存流程",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	option: {
		url: `${config.API_URL}/sysflowinfo/option`,
		name: "列表",
		get: async function (data) {
			return await http.get(this.url, data);
		},
	}
}


