import config from "@/config";
import http from "@/utils/request";
export default {
	byuser: {
		url: `${config.API_URL}/mgrUserRole/byuser/`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	save: {
		url: `${config.API_URL}/mgrUserRole/save`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
};
