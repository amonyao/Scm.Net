import API from "@/api";

//审批工作流人员/组织选择器配置

export default {
	//配置接口正常返回代码
	successCode: 200,
	//配置组织
	group: {
		//请求接口对象
		apiObj: API.urrole.list,
		//接受数据字段映射
		parseData: function (res) {
			return {
				rows: res.data,
				msg: res.message,
				code: res.code
			}
		},
		//显示数据字段映射
		props: {
			key: 'id',
			label: 'names',
			children: 'children'
		}
	},
	//配置用户
	user: {
		apiObj: API.uruser.page,
		pageSize: 20,
		parseData: function (res) {
			return {
				rows: res.data.items,
				total: res.data.total,
				msg: res.message,
				code: res.code
			}
		},
		props: {
			key: 'id',
			label: 'names',
		},
		request: {
			page: 'page',
			pageSize: 'pageSize',
			groupId: 'groupId',
			keyword: 'keyword'
		}
	},
	//配置角色
	role: {
		//请求接口对象
		apiObj: API.urrole.option,
		//接受数据字段映射
		parseData: function (res) {
			return {
				rows: res.data,
				msg: res.message,
				code: res.code
			}
		},
		//显示数据字段映射
		props: {
			key: 'id',
			label: 'names',
			children: 'children'
		}
	}
}
