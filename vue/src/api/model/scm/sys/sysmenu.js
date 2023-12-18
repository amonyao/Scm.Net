import config from "@/config";
import http from "@/utils/request";
export default {
	authority: {
		url: `${config.API_URL}/sysmenu/authoritymenu`,
		name: "获得权限",
		get: async function () {
			return await http.get(this.url);
		},
	},
};
