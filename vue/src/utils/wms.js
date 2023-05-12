import config from "@/config"
import http from "@/utils/request"
import scm from "@/utils/scm"

const wms = {};

wms.regex_area_codes = /^[-_0-9a-zA-Z]{1,32}$/;

wms.KEY_NODE = "node";
wms.KEY_OPT = "opt";
wms.KEY_DATA = "data";

/* 选项 */
wms.OPT_OPTION = "a1";

/* 搜索 */
wms.OPT_SEARCH = "a2";


/* 加载 */
wms.OPT_LOAD = "b1";

/* 页面 */
wms.OPT_PAGE = "b2";

/* 查看 */
wms.OPT_VIEW = "b3";

/* 编辑 */
wms.OPT_EDIT = "b4";

/* 详情 */
wms.OPT_HANDLE = "b5";

/* 头档 */
wms.OPT_HEADER = "b6";

/* 明细 */
wms.OPT_DETAIL = "b7";


/* 选中 */
wms.OPT_SELECT = "c1";

/* 追加 */
wms.OPT_APPEND = "c2";

/* 更新 */
wms.OPT_UPDATE = "c3";

/* 移除（逻辑）*/
wms.OPT_REMOVE = "c4";

/* 删除（物理） */
wms.OPT_DELETE = "c5";

/** 创建 */
wms.OPT_CREATE = "c6";

/* 变更 */
wms.OPT_CAHNGE = "c7";

/* 通知 */
wms.OPT_NOTICE = "c8";


/* 提交 */
wms.OPT_COMMIT = "d6";

wms.SI_NODE_AUDIT = 1010;
wms.SI_NODE_CHECK = 1020;
wms.SI_NODE_RECEIVE = 1030;
wms.SI_NODE_DOCK = 1040;
wms.SI_NODE_PUTAWAY = 1050;

wms.SI_HANDLE_INIT = 100;
wms.SI_HANDLE_EDIT_DOING = 105;
wms.SI_HANDLE_AUDIT_TODO = 110;
wms.SI_HANDLE_AUDIT_DOING = 115;
wms.SI_HANDLE_QC_TODO = 120;
wms.SI_HANDLE_QC_DOING = 125;
wms.SI_HANDLE_RECEIVING_TODO = 130;
wms.SI_HANDLE_RECEIVING_DOING = 135;
wms.SI_HANDLE_DOCKING_TODO = 140;
wms.SI_HANDLE_DOCKING_DOING = 145;
wms.SI_HANDLE_PUTAWAY_TODO = 150;
wms.SI_HANDLE_PUTAWAY_DOING = 155;
wms.SI_HANDLE_DONE = 160;

wms.SO_NODE_AUDIT = 2010;
wms.SO_NODE_ANALYSE = 2020;
wms.SO_NODE_ALLOCATE = 2030;
wms.SO_NODE_WAVING = 2040;
wms.SO_NODE_PICKING = 2050;
wms.SO_NODE_SORTING = 2060;
wms.SO_NODE_CHECKING = 2070;
wms.SO_NODE_VANNING = 2080;
wms.SO_NODE_PACKING = 2090;
wms.SO_NODE_WEIGHING = 2100;
wms.SO_NODE_MEASURING = 2110;
wms.SO_NODE_MATCHING = 2120;
wms.SO_NODE_LOADING = 2130;
wms.SO_NODE_DISPATCH = 2140;
wms.SO_NODE_RECEIPT = 2150;

wms.SO_HANDLE_INIT = 200;
wms.SO_HANDLE_EDIT_DOING = 205;
wms.SO_HANDLE_AUDIT_TODO = 210;
wms.SO_HANDLE_AUDIT_DOING = 215;
wms.SO_HANDLE_ANALYSE_TODO = 220;
wms.SO_HANDLE_ANALYSE_DOING = 225;
wms.SO_HANDLE_ALLOCATE_TODO = 230;
wms.SO_HANDLE_ALLOCATE_DOING = 235;
wms.SO_HANDLE_WAVING_TODO = 240;
wms.SO_HANDLE_WAVING_DOING = 245;
wms.SO_HANDLE_PICKING_TODO = 250;
wms.SO_HANDLE_PICKING_DOING = 255;
wms.SO_HANDLE_SORTING_TODO = 260;
wms.SO_HANDLE_SORTING_DOING = 265;
wms.SO_HANDLE_CHECKING_TODO = 270;
wms.SO_HANDLE_CHECKING_DOING = 275;
wms.SO_HANDLE_VANNING_TODO = 280;
wms.SO_HANDLE_VANNING_DOING = 285;
wms.SO_HANDLE_PACKING_TODO = 290;
wms.SO_HANDLE_PACKING_DOING = 295;
wms.SO_HANDLE_WEIGHING_TODO = 300;
wms.SO_HANDLE_WEIGHING_DOING = 305;
wms.SO_HANDLE_MEASURING_TODO = 310;
wms.SO_HANDLE_MEASURING_DOING = 315;
wms.SO_HANDLE_MATCHING_TODO = 320;
wms.SO_HANDLE_MATCHING_DOING = 325;
wms.SO_HANDLE_LOADING_TODO = 330;
wms.SO_HANDLE_LOADING_DOING = 335;
wms.SO_HANDLE_DISPATCH_TODO = 340;
wms.SO_HANDLE_DISPATCH_DOING = 345;
wms.SO_HANDLE_RECEIPT_TODO = 350;
wms.SO_HANDLE_RECEIPT_DOING = 355;
wms.SO_HANDLE_DONE = 360;

wms.to_params = function (data) {
    return { 'node': '', 'opt': '', 'data': JSON.stringify(data) };
};

wms.to_keep_qty = function (uom, qty) {
    return uom.qty * qty;
};

wms.to_view_uom = function (uom, qty) {
    return (qty / uom.qty) + uom.names;
};

wms.list_owner = async function (list, id, all) {
    var res = await http.get(`${config.API_URL}/resroowner/option/` + '0');
    scm.prepare(list, res, all);
};

wms.list_producer = async function (list, ownerId, all) {
    var res = await http.get(`${config.API_URL}/resroproducer/option/` + ownerId);
    scm.prepare(list, res, all);
};

wms.list_supplier = async function (list, ownerId, all) {
    var res = await http.get(`${config.API_URL}/resrosupplier/option/` + ownerId);
    scm.prepare(list, res, all);
};

wms.list_brand = async function (list, producerId, all = true) {
    var res = await http.get(`${config.API_URL}/resrobrand/option/` + producerId);
    scm.prepare(list, res, all);
};

wms.list_category = async function (list, ownerId, all = true) {
    var res = await http.get(`${config.API_URL}/resrocategory/option/` + ownerId);
    scm.prepare(list, res, all);
};

wms.list_merchant = async function (list, id, all = true) {
    //var res = await http.get(`${config.API_URL}/resrmmerchant/option/` + '0');
    var res = await http.get(`${config.API_URL}/resroowner/option/` + '0');
    scm.prepare(list, res, all);
};

wms.list_channels = async function (list, merchantId, all = true) {
    var res = await http.get(`${config.API_URL}/resrmchannels/option/` + merchantId);
    scm.prepare(list, res, all);
};

wms.list_business = async function (list, channelsId, all = true) {
    var res = await http.get(`${config.API_URL}/resrmbusiness/option/` + channelsId);
    scm.prepare(list, res, all);
};

wms.list_sys_shipper = async function (list, id, all = true) {
    var res = await http.get(`${config.API_URL}/sysshipper/option/` + id);
    scm.prepare(list, res, all);
};

wms.list_sys_express = async function (list, shipperId, all = true) {
    var res = await http.get(`${config.API_URL}/sysexpress/option/` + shipperId);
    scm.prepare(list, res, all);
};

wms.list_res_shipper = async function (list, id, all = true) {
    var res = await http.get(`${config.API_URL}/reswhshipper/option/` + id);
    scm.prepare(list, res, all);
};

wms.list_res_express = async function (list, shipperId, all = true) {
    var res = await http.get(`${config.API_URL}/reswhexpress/option/` + shipperId);
    scm.prepare(list, res, all);
};

wms.list_store = async function (list, id, all = true) {
    var res = await http.get(`${config.API_URL}/cfghwstore/option/` + id);
    scm.prepare(list, res, all);
};

wms.list_area = async function (list, storeId, all = true) {
    var res = await http.get(`${config.API_URL}/cfghwarea/option/` + storeId);
    scm.prepare(list, res, all);
};

wms.list_aisle = async function (list, areaId, all = true) {
    var res = await http.get(`${config.API_URL}/cfghwaisle/option/` + areaId);
    scm.prepare(list, res, all);
};

wms.list_bay = async function (list, aisleId, all = true) {
    var res = await http.get(`${config.API_URL}/cfghwbay/option/` + aisleId);
    scm.prepare(list, res, all);
};

wms.list_layer = async function (list, bayId, all = true) {
    var res = await http.get(`${config.API_URL}/cfghwlayer/option/` + bayId);
    scm.prepare(list, res, all);
};

wms.list_bin = async function (list, layerId, all = true) {
    var res = await http.get(`${config.API_URL}/cfghwbin/option/` + layerId);
    scm.prepare(list, res, all);
};

wms.list_uom = async function (list, types, all = true) {
    var res = await http.get(`${config.API_URL}/sysuom/option/` + types);
    scm.prepare(list, res, all);
};

wms.list_bat = async function (list, types, all = true) {
    var res = await http.get(`${config.API_URL}/sysbat/option/` + types);
    scm.prepare(list, res, all);
};

wms.list_sku_package = async function (list, types, all = true) {
    var res = await http.get(`${config.API_URL}/skupackage/option/` + types);
    scm.prepare(list, res, all);
};

wms.list_sku_barcode = async function (list, types, all = true) {
    var res = await http.get(`${config.API_URL}/skubarcode/option/` + types);
    scm.prepare(list, res, all);
};

export default wms;