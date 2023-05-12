import config from "@/config";
import http from "@/utils/request";
export default {
	byrole: {
		url: `${config.API_URL}/mgrroleauth/byrole/`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	save: {
		url: `${config.API_URL}/mgrroleauth/save`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
};
