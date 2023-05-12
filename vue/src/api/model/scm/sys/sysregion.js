import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/sysregion/pages`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url,params);
		}
	},
	list: {
		url: `${config.API_URL}/sysregion/list`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url,params);
		}
	},
	add: {
		url: `${config.API_URL}/sysregion`,
		name: "添加",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	model: {
		url: `${config.API_URL}/sysregion/`,
		name: "查询一条",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	update: {
		url: `${config.API_URL}/sysregion`,
		name: "修改",
		put: async function(data){
			return await http.put(this.url,data);
		}
	},
	status: {
		url: `${config.API_URL}/sysregion/status`,
		name: "状态",
		post: async function(data){
			return await http.post(this.url,data);
		}
	},
	option: {
		url: `${config.API_URL}/sysregion/option/`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	delete: {
		url: `${config.API_URL}/sysregion`,
		name: "删除",
		delete: async function(params){
			return await http.delete(this.url+'?ids='+params);
		}
	},
	sort: {
		url: `${config.API_URL}/sysregion/colsort`,
		name: "修改",
		post: async function(data){
			return await http.post(this.url,data);
		}
	},
	sublist: {
		url: `${config.API_URL}/sysregion/listbypid`,
		name: "查询一条",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	DEF_PID:'1000000000000000002',
}


