import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/mgrregion/pages`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url,params);
		}
	},
	list: {
		url: `${config.API_URL}/mgrregion/list`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url,params);
		}
	},
	add: {
		url: `${config.API_URL}/mgrregion`,
		name: "添加",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	edit: {
		url: `${config.API_URL}/mgrregion/edit/`,
		name: "编辑",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	view: {
		url: `${config.API_URL}/mgrregion/view/`,
		name: "查看",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	update: {
		url: `${config.API_URL}/mgrregion`,
		name: "修改",
		put: async function(data){
			return await http.put(this.url,data);
		}
	},
	status: {
		url: `${config.API_URL}/mgrregion/status`,
		name: "状态",
		post: async function(data){
			return await http.post(this.url,data);
		}
	},
	option: {
		url: `${config.API_URL}/mgrregion/option/`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	delete: {
		url: `${config.API_URL}/mgrregion`,
		name: "删除",
		delete: async function(params){
			return await http.delete(this.url+'?ids='+params);
		}
	},
	sort: {
		url: `${config.API_URL}/mgrregion/colsort`,
		name: "修改",
		post: async function(data){
			return await http.post(this.url,data);
		}
	},
	DEF_PID:'1000000000000000002',
}


