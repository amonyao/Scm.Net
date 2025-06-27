
delete from scm_ur_unit where id > 1000000000000001040;
update scm_ur_user set pass='02118dx6yf969eef6ecadfqf63c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c928nf2';
drop table scm_log_hb;

DELETE from scm_calendar where unit_id not in (select a.id from scm_ur_unit a);
DELETE from scm_calendar_user where user_id not in (select a.id from scm_ur_user a);

delete from scm_cfg_date_theme;
delete from scm_cfg_export_detail;
delete from scm_cfg_export_header;
delete from scm_cfg_menu where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_cfg_user_theme where user_id not in (select a.id from scm_ur_user a);

delete from scm_dev_db;
delete from scm_dev_sql;

delete from scm_flow_auth;
delete from scm_flow_data_detail;
delete from scm_flow_data_header;
delete from scm_flow_data_result;
delete from scm_flow_info;
delete from scm_flow_node;
delete from scm_flow_order;

delete from scm_log_api;
delete from scm_log_oauth;
delete from scm_log_sms;
delete from scm_log_user;

delete from scm_qcs_detail;
delete from scm_qcs_header;
delete from scm_qcs_queue;

delete from scm_res_cat where unit_id not in (select a.id from scm_ur_unit a);
DELETE from scm_res_product;
DELETE from scm_res_product_images;
DELETE from scm_res_service;
DELETE from scm_res_sms;
DELETE from scm_res_tag;

DELETE from scm_sys_config where unit_id not in (select a.id from scm_ur_unit a);
DELETE from scm_sys_config_cat;
DELETE from scm_sys_config_key;
DELETE from scm_sys_dic_header where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_sys_dic_detail where dic_header_id not in (select a.id from scm_sys_dic_header a);
DELETE from scm_sys_note where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_sys_pv_header;
delete from scm_sys_pv_detail;
delete from scm_sys_region;
delete from scm_sys_table_detail;
delete from scm_sys_table_header;
delete from scm_sys_task;
delete from scm_sys_uid where k like 'wms_%';
delete from scm_sys_uid where k like 'fes_%';
delete from scm_sys_uid where k like 'pos_%';
delete from scm_sys_uid where k like 'erp_%';
delete from scm_sys_ver_client ;
delete from scm_sys_ver_detail;
delete from scm_sys_ver_header;
delete from scm_sys_ver_info;

delete from scm_ur_group where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_ur_organize where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_ur_position where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_ur_role where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_ur_role_auth where role_id not in (select a.id from scm_ur_role a);
delete from scm_ur_role_conflict;
delete from scm_ur_role_data;

delete from scm_ur_user where unit_id not in (select a.id from scm_ur_unit a);
delete from scm_ur_user_data where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_group where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_oauth where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_organize where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_position where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_role where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_token where user_id not in (select a.id from scm_ur_user a);
delete from scm_ur_user_wx;
delete from scm_vote_detail;
delete from scm_vote_header;
delete from scm_vote_result;

drop table scm_sys_region;
CREATE TABLE scm_sys_region (
  id bigint(20) NOT NULL,
  unit_id bigint(20) NOT NULL DEFAULT 0,
  codes varchar(16) DEFAULT NULL,
  codec varchar(32) DEFAULT NULL,
  names varchar(64) DEFAULT NULL,
  namef varchar(64) NOT NULL,
  namee varchar(128) DEFAULT NULL,
  pid bigint(20) NOT NULL DEFAULT 1,
  ParentIdList varchar(512) DEFAULT NULL,
  lv int(11) NOT NULL DEFAULT 1,
  od int(11) DEFAULT 1,
  lng varchar(64) DEFAULT NULL,
  lat varchar(64) DEFAULT NULL,
  postcode varchar(8) DEFAULT NULL,
  p_status tinyint(4) NOT NULL DEFAULT 0,
  row_status tinyint(4) NOT NULL DEFAULT 0,
  row_delete tinyint(4) NOT NULL DEFAULT 0,
  update_time bigint(20) NOT NULL DEFAULT 0,
  update_user bigint(20) DEFAULT NULL,
  create_time bigint(20) NOT NULL DEFAULT 0,
  create_user bigint(20) DEFAULT NULL,
  PRIMARY KEY (id)
);