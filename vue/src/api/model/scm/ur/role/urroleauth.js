import config from "@/config";
import http from "@/utils/request";
export default {
	admin: {
		url: `${config.API_URL}/urroleauth/byadmin/`,
		name: "根据管理员查询授权角色",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	role: {
		url: `${config.API_URL}/urroleauth/byrole/`,
		name: "根据角色查询菜单",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	plusAdmin: {
		url: `${config.API_URL}/urroleauth/role`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	plusRole: {
		url: `${config.API_URL}/urroleauth`,
		name: "添加",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
};
