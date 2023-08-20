import config from "@/config"
import http from "@/utils/request"
import scm from "@/utils/scm"

export default {
	page: {
		url: `${config.API_URL}/cmsresdynasty/pages`,
		name: "查询",
		get: async function(params){
			return await http.get(this.url,params);
		}
	},
	list: {
		url: `${config.API_URL}/cmsresdynasty/list`,
		name: "列表",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	view: {
		url: `${config.API_URL}/cmsresdynasty/view/`,
		name: "查看",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	edit: {
		url: `${config.API_URL}/cmsresdynasty/edit/`,
		name: "编辑",
		get: async function(params){
			return await http.get(this.url + params);
		}
	},
	add: {
		url: `${config.API_URL}/cmsresdynasty`,
		name: "新增",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	update: {
		url: `${config.API_URL}/cmsresdynasty`,
		name: "更新",
		put: async function(data){
			return await http.put(this.url,data);
		}
	},
	save:{
		url: `${config.API_URL}/cmsresdynasty`,
		name: "保存",
		put: async function (data) {
			return await http.put(this.url, data);
		}
	},
	status: {
		url: `${config.API_URL}/cmsresdynasty/status`,
		name: "状态",
		post: async function(data){
			return await http.post(this.url, data);
		}
	},
	delete: {
		url: `${config.API_URL}/cmsresdynasty`,
		name: "删除",
		delete: async function(data){
			return await http.delete(this.url,data);
		}
	},
	option: {
		url: `${config.API_URL}/cmsresdynasty/option`,
		name: "列表",
		get: async function(data){
			return await http.get(this.url,data);
		}
	},
	list_option: async function (data, list, opt) {
		var url = `${config.API_URL}/cmsresdynasty/option`;
		var res = await http.get(url, data);
		scm.prepare(list, res, opt);
	}
}