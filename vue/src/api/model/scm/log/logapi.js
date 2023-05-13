import config from "@/config"
import http from "@/utils/request"
export default {
	page: {
		url: `${config.API_URL}/alogapi/pages`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url,params);
		}
	},
	chart: {
		url: `${config.API_URL}/alogapi/chart`,
		name: "图表",
		get: async function(){
			return await http.get(this.url);
		}
	},
	add: {
		url: `${config.API_URL}/alogapi`,
		name: "添加",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	model: {
		url: `${config.API_URL}/alogapi/`,
		name: "查询一条",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	update: {
		url: `${config.API_URL}/alogapi`,
		name: "修改",
		put: async function(data){
			return await http.put(this.url,data);
		}
	},
	delete: {
		url: `${config.API_URL}/alogapi`,
		name: "删除",
		delete: async function(params){
			return await http.delete(this.url+'?ids='+params);
		}
	},
	clear: {
		url: `${config.API_URL}/alogapi/clear/`,
		name: "清空",
		delete: async function(){
			return await http.delete(this.url);
		}
	}
}


