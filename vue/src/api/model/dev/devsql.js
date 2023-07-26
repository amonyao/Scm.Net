import config from "@/config"
import http from "@/utils/request"
export default {
	list: {
		url: `${config.API_URL}/devsql/table/`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	column: {
		url: `${config.API_URL}/devsql/column/`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		}
	},
	execute: {
		url: `${config.API_URL}/devsql/execute`,
		name: "执行",
		post: async function (params) {
			return await http.post(this.url, params);
		}
	},
}


