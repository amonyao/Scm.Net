import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/mgrfeedback/pages`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	view: {
		url: `${config.API_URL}/mgrfeedback/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	save: {
		url: `${config.API_URL}/mgrfeedback/save`,
		name: "保存",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	handle: {
		url: `${config.API_URL}/mgrfeedback/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
};
