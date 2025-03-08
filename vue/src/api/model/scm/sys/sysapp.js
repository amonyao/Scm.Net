import config from "@/config";
import http from "@/utils/request";
export default {
	model: {
		url: `${config.API_URL}/sysapp/`,
		name: "查询一条",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	info: {
		url: `${config.API_URL}/sysapp/info`,
		name: "其它信息",
		get: async function (data) {
			return await http.get(this.url, data);
		},
	}
};
