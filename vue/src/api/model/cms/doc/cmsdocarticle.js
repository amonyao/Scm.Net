import config from "@/config";
import http from "@/utils/request";
export default {
	page: {
		url: `${config.API_URL}/cmsdocarticle/pages`,
		name: "查询",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	list: {
		url: `${config.API_URL}/cmsdocarticle/list`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url, params);
		},
	},
	model: {
		url: `${config.API_URL}/cmsdocarticle/`,
		name: "列表",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	view: {
		url: `${config.API_URL}/cmsdocarticle/view/`,
		name: "查看",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	edit: {
		url: `${config.API_URL}/cmsdocarticle/edit/`,
		name: "编辑",
		get: async function (params) {
			return await http.get(this.url + params);
		},
	},
	add: {
		url: `${config.API_URL}/cmsdocarticle`,
		name: "新增",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	addpoetry: {
		url: `${config.API_URL}/cmsdocarticle/poetry`,
		name: "新增",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	addlitera: {
		url: `${config.API_URL}/cmsdocarticle/litera`,
		name: "新增",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	update: {
		url: `${config.API_URL}/cmsdocarticle`,
		name: "更新",
		put: async function (data) {
			return await http.put(this.url, data);
		},
	},
	save: {
		url: `${config.API_URL}/cmsdocarticle`,
		name: "保存",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	savenotes: {
		url: `${config.API_URL}/cmsdocarticle/savenotes`,
		name: "保存",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	status: {
		url: `${config.API_URL}/cmsdocarticle/status`,
		name: "状态",
		post: async function (data) {
			return await http.post(this.url, data);
		},
	},
	delete: {
		url: `${config.API_URL}/cmsdocarticle`,
		name: "删除",
		delete: async function (data) {
			return await http.delete(this.url, data);
		},
	},
	daily: {
		url: `${config.API_URL}/cmsdocarticle/daily`,
		name: "每日推荐",
		get: async function (data) {
			return await http.get(this.url, data);
		},
	},
	items: {
		url: `${config.API_URL}/cmsdocarticle/items/`,
		name: "详情",
		get: async function (data) {
			return await http.get(this.url + data);
		},
	},
};
