import config from "@/config";
import http from "@/utils/request";
export default {
	model: {
		url: `${config.API_URL}/about`,
		name: "详情",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	info: {
		url: `${config.API_URL}/about/info`,
		name: "详情",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
};
