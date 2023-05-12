import config from "@/config"
import http from "@/utils/request"

export default {
	list: {
		url: `${config.API_URL}/config/list/`,
		name: "获取配置",
		get: async function (data) {
			return await http.get(this.url + data);
		}
	},
	save: {
		url: `${config.API_URL}/config/save`,
		name: "更新配置",
		post: async function (data) {
			return await http.post(this.url, data);
		}
	},
}
