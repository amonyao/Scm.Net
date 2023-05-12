import config from "@/config"
import http from "@/utils/request"

const scm = {};

scm.DEF_INT = 0;
scm.SYS_ID = '1000000000000000001';

scm.regex_id = /^[1-9]\d{15,18}$/;
scm.regex_codec = /^[-_0-9a-zA-Z]{1,32}$/;
scm.regex_namec = /^$\w{1,64}/;
scm.regex_int = /^[1-9]\d*$/;

scm.OPTION_ALL = { 'label': '所有', 'id': '0' };
scm.OPTION_ONE = { 'label': '请选择', 'id': '0' };

scm.is_valid_id = function (id) {
    return scm.regex_id.test(id);
};

scm.status_item = async function (dom, http, data, status) {
    if (!data.id) {
        return;
    }

    let param = { 'ids': [data.id], 'status': status };
    var res = await http.post(param);
    if (res.code == 200) {
        //dom.$refs.table.refresh();
        //dom.$message.success("操作完成。");
    } else {
        dom.$alert(res.message, "提示", { type: "error" });
    }
};

scm.status_list = function (dom, http, list, status) {
    let text = status == 1 ? '启用' : '禁用';
    dom.$confirm(
        `确定${text}选中的 ${list.length} 项吗？`,
        "提示",
        {
            type: "warning",
            confirmButtonText: '确定',
            cancelButtonText: '取消',
        }
    )
        .then(async () => {
            const loading = dom.$loading();
            let ids = [];
            list.forEach((element) => {
                ids.push(element.id);
            });
            let param = { 'ids': ids, 'status': status };
            var res = await http.post(param);
            if (res.code == 200) {
                dom.$refs.table.refresh();
                loading.close();
                dom.$message.success("操作完成。");
            } else {
                dom.$alert(res.message, "提示", { type: "error" });
            }
        })
        .catch(() => { });
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
    dom.$confirm(
        `确定删除选中的 ${list.length} 项吗？`,
        "提示",
        {
            type: "warning",
            confirmButtonText: '确定',
            cancelButtonText: '取消',
        }
    )
        .then(async () => {
            var loading = dom.$loading();
            let ids = [];
            list.forEach((element) => {
                ids.push(element.id);
            });
            var res = http.delete(
                ids.join(",")
            );
            if (res.code == 200) {
                dom.$refs.table.refresh();
                loading.close();
                dom.$message.success("操作成功。");
            } else {
                dom.$alert(res.message, "提示", { type: "error" });
            }
        })
        .catch(() => { });
};

scm.option_all = function (arr) {
    arr.unshift(scm.OPTION_ALL);
    return arr;
};
scm.option_one = function (arr) {
    arr.unshift(scm.OPTION_ONE);
    return arr;
};

scm.option_value_all = function (arr) {
    arr.unshift(scm.OPTION_ALL);
    return arr;
};
scm.option_value_one = function (arr) {
    arr.unshift(scm.OPTION_ONE);
    return arr;
};

scm.getDate = function () {
    var date = new Date();
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? '0' + m : m) + '-' + (d < 10 ? '0' + d : d);
};

scm.getTime = function () {
    var date = new Date()
    var y = date.getFullYear()
    var m = date.getMonth() + 1
    m = m < 10 ? ('0' + m) : m
    var d = date.getDate()
    d = d < 10 ? ('0' + d) : d
    alert(y + m + d)
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

scm.prepare = function (list, res, all) {
    list.length = 0;
    if (all) {
        scm.option_all(list);
    } else {
        scm.option_one(list);
    }
    if (res.code != 200) {
        return;
    }
    res.data.some(m => { list.push(m); });
};

scm.list_dic = async function (list, key, all) {
    var res = await http.get(`${config.API_URL}/scmdic/option/` + key);
    scm.prepare(list, res, all);
};

scm.list_status = async function (list, all) {
    scm.list_dic(list, 'status', all);
};

scm.list_sex = async function (list, all) {
    scm.list_dic(list, 'sex', all);
};

scm.list_app = async function (list, all) {
    scm.list_dic(list, 'app', all);
};

scm.list_region = async function (list, pid, all) {
    if (!pid) {
        return;
    }

    var res = await http.get(`${config.API_URL}/sysregion/option/` + pid);
    scm.prepare(list, res, all);
};

scm.list_nation = async function (list, pid, all) {
    if (!pid) {
        return;
    }

    var res = await http.get(`${config.API_URL}/cjgresnation/option/` + pid);
    scm.prepare(list, res, all);
};

scm.list_dynasty = async function (list, pid, all) {
    if (!pid) {
        return;
    }

    var res = await http.get(`${config.API_URL}/cjgresdynasty/option/` + pid);
    scm.prepare(list, res, all);
};

scm.list_author = async function (list, pid, all) {
    if (!pid) {
        return;
    }

    var res = await http.get(`${config.API_URL}/cjgresauthor/option/` + pid);
    scm.prepare(list, res, all);
};

scm.list_origion = async function (list, pid, all) {
    if (!pid) {
        return;
    }

    var res = await http.get(`${config.API_URL}/cjgresorigion/option/` + pid);
    scm.prepare(list, res, all);
};

export default scm;