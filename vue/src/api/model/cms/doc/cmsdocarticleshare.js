import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/cmsdocarticleshare/pages`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
};
