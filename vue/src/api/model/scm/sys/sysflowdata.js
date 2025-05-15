import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/sysflowdata/page`,
		name: "分页",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	list: {
		url: `${config.API_URL}/sysflowdata/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	view: {
		url: `${config.API_URL}/sysflowdata/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		}
	},
	status: {
		url: `${config.API_URL}/sysflowdata/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/sysflowdata`,
		name: "删除",
		delete: async function (data) {
			return await http.delete(this.url, data);
		}
	},
	result: {
		url: `${config.API_URL}/sysflowdata/result/`,
		name: "删除",
		get: async function (data) {
			return await http.get(this.url + data);
		}
	},
}