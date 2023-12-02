import config from "@/config";
import http from "@/utils/request";
import tool from "@/utils/tool";

const scm = {};

scm.DEF_INT = 0;
scm.SYS_ID = "1000000000000000001";

scm.REGEX_ID = /^[1-9]\d{15,18}$/;
scm.REGEX_INT = /^[1-9]\d*$/;
scm.REGEX_CODEC = /^[-_0-9a-zA-Z]{1,32}$/;
scm.REGEX_NAMEC = /^$\w{1,64}/;
scm.REGEX_NUMBER = /^\d+$/;

scm.OPTION_ALL = { label: "所有", id: "0", value: "0" };
scm.OPTION_ONE = { label: "请选择", id: "0", value: "0" };
scm.OPTION_ALL_INT = { label: "所有", id: "0", value: 0 };
scm.OPTION_ONE_INT = { label: "请选择", id: "0", value: 0 };

scm.catch = [];

scm.is_valid_id = function (text) {
	return scm.REGEX_ID.test(text);
};

scm.is_valid_int = function (text) {
	return scm.REGEX_INT.test(text);
};

scm.is_valid_codec = function (text) {
	return scm.REGEX_CODEC.test(text);
};

scm.is_valid_namec = function (text) {
	return scm.REGEX_NAMEC.test(text);
};

scm.encode_pass = function (pass) {
	var len1 = 4;
	var len2 = 4;
	var len3 = 4;
	var len = pass.length >> 1;

	var idx1 = Math.floor(Math.random() * len);
	var idx2 = Math.floor(Math.random() * len) + idx1;

	var tmp1 = tool.randomString(len1, false);
	var tmp2 = tool.randomString(len2, false);
	var tmp3 = tool.randomString(len3, false);

	var buf = "";
	var num1 = idx1.toString(16);
	var num2 = (len1 + idx2).toString(16);
	buf += (num1.length < 2 ? "0" : "") + num1;
	buf += (num2.length < 2 ? "0" : "") + num2;
	buf += pass.slice(0, idx1);
	buf += tmp1;
	buf += pass.slice(idx1, idx2);
	buf += tmp2;
	buf += pass.slice(idx2);
	buf += tmp3;
	return buf;
};

scm.status_item = async function (dom, http, data, status) {
	if (!data.id) {
		return;
	}

	let param = { ids: [data.id], status: status };
	var res = await http.post(param);
	if (res.code == 200) {
		//dom.$refs.table.refresh();
		//dom.$message.success("操作完成。");
	} else {
		dom.$alert(res.message, "提示", { type: "error" });
	}
};

scm.status_list = function (dom, http, list, status) {
	let text = status == 1 ? "启用" : "禁用";
	dom.$confirm(`确定${text}选中的 ${list.length} 项吗？`, "提示", {
		type: "warning",
		confirmButtonText: "确定",
		cancelButtonText: "取消",
	})
		.then(async () => {
			const loading = dom.$loading();
			let ids = [];
			list.forEach((element) => {
				ids.push(element.id);
			});
			let param = { ids: ids, status: status };
			var res = await http.post(param);
			if (res.code == 200) {
				dom.$refs.table.refresh();
				loading.close();
				dom.$message.success("操作完成。");
			} else {
				dom.$alert(res.message, "提示", { type: "error" });
			}
		})
		.catch(() => {});
};

scm.delete_item = async function (dom, http, data) {
	if (!data.id) {
		return;
	}

	var res = await http.delete(data.id);
	if (res.code == 200) {
		dom.$refs.table.refresh();
		dom.$message.success("操作完成。");
	} else {
		dom.$alert(res.message, "提示", { type: "error" });
	}
};

scm.delete_list = function (dom, http, list) {
	dom.$confirm(`确定删除选中的 ${list.length} 项吗？`, "提示", {
		type: "warning",
		confirmButtonText: "确定",
		cancelButtonText: "取消",
	})
		.then(async () => {
			var loading = dom.$loading();
			let ids = [];
			list.forEach((element) => {
				ids.push(element.id);
			});
			var res = http.delete(ids.join(","));
			if (res.code == 200) {
				dom.$refs.table.refresh();
				loading.close();
				dom.$message.success("操作成功。");
			} else {
				dom.$alert(res.message, "提示", { type: "error" });
			}
		})
		.catch(() => {});
};

scm.getDate = function () {
	var date = new Date();
	var y = date.getFullYear();
	var m = date.getMonth() + 1;
	var d = date.getDate();
	return y + "-" + (m < 10 ? "0" + m : m) + "-" + (d < 10 ? "0" + d : d);
};

scm.getTime = function () {
	var date = new Date();
	var y = date.getFullYear();
	var m = date.getMonth() + 1;
	m = m < 10 ? "0" + m : m;
	var d = date.getDate();
	d = d < 10 ? "0" + d : d;
	alert(y + m + d);
};

scm.format = function (str, len, pad) {
	pad = pad || "*";
	if (!str) {
		return pad.repeat(len);
	}

	const t = str.length;
	if (t > len) {
		return str.substring(0, len);
	}
	if (t < len) {
		return str.padEnd(len, pad);
	}

	return str;
};

/**
 * 追加【所有】选项
 * @param {Array} arr
 * @returns
 */
scm.option_all = function (arr) {
	if (arr != null) {
		arr.unshift(scm.OPTION_ALL);
	}
	return arr;
};

/**
 * 追加【请选择】选项
 * @param {Array} arr
 * @returns
 */
scm.option_one = function (arr) {
	if (arr != null) {
		arr.unshift(scm.OPTION_ONE);
	}
	return arr;
};

/**
 * 下拉列表准备
 * @param {*} list
 * @param {*} res
 * @param {*} all
 * @returns
 */
scm.prepare = function (list, res, all) {
	if (list == null) {
		return;
	}

	list.length = 0;
	if (all) {
		scm.option_all(list);
	} else if (all == false) {
		scm.option_one(list);
	}
	if (res.code != 200) {
		return;
	}
	res.data.some((m) => {
		list.push(m);
	});
};

/**
 * 获取字典列表
 * @param {Array} list 输入列表
 * @param {String} key 字典值
 * @param {Boolean} all 默认值类型：true:所有，false:请选择
 * @param {Boolean} useCatch 是否启用缓冲
 * @returns
 */
scm.list_dic = async function (list, key, all, useCatch) {
	if (!list) {
		return;
	}

	list.length = 0;
	if (all) {
		list.push(scm.OPTION_ALL_INT);
	} else if (all == false) {
		list.push(scm.OPTION_ONE_INT);
	}

	var data = useCatch ? scm.catch[key] : null;
	if (data == null) {
		var res = await http.get(`${config.API_URL}/scmdic/option/` + key);
		if (!res || res.code != 200) {
			return;
		}

		data = res.data;
		if (useCatch) {
			scm.catch[key] = data;
		}
	}

	data.some((m) => {
		list.push(m);
	});
};

/**
 * 获取数据状态列表
 * @param {*} list
 * @param {*} all
 */
scm.list_status = async function (list, all) {
	scm.list_dic(list, "status", all, true);
};

/**
 * 获取性别列表
 * @param {*} list
 * @param {*} all
 */
scm.list_sex = async function (list, all) {
	scm.list_dic(list, "sex", all, true);
};

/**
 * 获取字典名称
 * @param {Array} list 字典列表
 * @param {String} key 字典键
 * @param {String} def 默认值
 * @returns
 */
scm.get_dic_names = function (list, key, def) {
	if (!list) {
		return def;
	}
	var obj = list.find((item) => {
		return item.value == key;
	});
	return obj ? obj.label : "";
};

/**
 * 获取下拉选项
 * @param {*} list
 * @param {*} api
 * @param {*} param
 * @param {*} all
 */
scm.list_option = async function (list, api, param, all) {
	var res = await api.get(param);
	scm.prepare(list, res, all);
};

/**
 * 获取应用列表
 * @param {*} list
 * @param {*} types
 * @param {*} all
 */
scm.list_app = async function (list, types, all) {
	var res = await http.get(`${config.API_URL}/devapp/option/` + types);
	scm.prepare(list, res, all);
};

/**
 * 获取区域列表
 * @param {输出目标列表} list
 * @param {上级ID} pid
 * @param {是否添加默认选项} all
 * @returns
 */
scm.list_region = async function (list, pid, all) {
	if (!pid) {
		return;
	}

	var res = await http.get(`${config.API_URL}/sysregion/option/` + pid);
	scm.prepare(list, res, all);
};

/**
 * 获取列表名称
 * @param {Array} options 字典列表
 * @param {String} key 字典键
 * @param {String} def 默认值
 * @returns
 */
scm.get_option_names = function (options, key, def) {
	if (!options) {
		return def;
	}
	key = "" + key;
	var obj = options.find((item) => {
		return item.value == key;
	});
	return obj ? obj.label : "";
};

export default scm;
