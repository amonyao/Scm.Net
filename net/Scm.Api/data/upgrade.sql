RENAME TABLE scm.cjg_doc_article TO scm.cms_doc_article;
ALTER TABLE scm.cms_doc_article CHANGE sub_title sub_title varchar(256) NULL AFTER title;
ALTER TABLE scm.cms_doc_article MODIFY COLUMN update_time bigint NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_doc_article MODIFY COLUMN create_time bigint NOT NULL COMMENT '创建时间';
ALTER TABLE scm.cms_doc_article MODIFY COLUMN row_status tinyint DEFAULT 0 NOT NULL;

RENAME TABLE scm.cjg_res_cat TO scm.cms_res_cat;
ALTER TABLE scm.cms_res_cat MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_res_cat MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL COMMENT '创建时间';
ALTER TABLE scm.cms_res_cat MODIFY COLUMN row_status tinyint DEFAULT 0 NOT NULL;

RENAME TABLE scm.cjg_res_dynasty TO scm.cms_res_dynasty;
ALTER TABLE scm.cms_res_dynasty MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_res_dynasty MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL COMMENT '创建时间';
ALTER TABLE scm.cms_res_dynasty MODIFY COLUMN row_status tinyint DEFAULT 0 NOT NULL;

RENAME TABLE scm.cjg_res_nation TO scm.cms_res_nation;
ALTER TABLE scm.cms_res_nation MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_res_nation MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL COMMENT '创建时间';
ALTER TABLE scm.cms_res_nation MODIFY COLUMN row_status tinyint DEFAULT 0 NOT NULL;

RENAME TABLE scm.cjg_res_origin TO scm.cms_res_origin;
ALTER TABLE scm.cms_res_origin MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_res_origin MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_res_origin CHANGE status row_status tinyint DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_res_origin MODIFY COLUMN row_status tinyint DEFAULT 0 NOT NULL;

RENAME TABLE scm.cjg_res_author TO scm.cms_res_author;
ALTER TABLE scm.cms_res_author MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_res_author MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL COMMENT '创建时间';

RENAME TABLE scm.cjg_res_tag TO scm.cms_res_tag;
ALTER TABLE scm.cms_res_tag MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_res_tag MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL COMMENT '创建时间';

RENAME TABLE scm.cjg_res_style TO scm.cms_res_style;
ALTER TABLE scm.cms_res_style MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_res_style MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_res_style MODIFY COLUMN row_status tinyint DEFAULT 0 NOT NULL;

RENAME TABLE scm.cjg_doc_article_tag TO scm.cms_doc_article_tag;
ALTER TABLE scm.cms_doc_article_tag MODIFY COLUMN update_time bigint DEFAULT 0 NOT NULL COMMENT '更新时间';
ALTER TABLE scm.cms_doc_article_tag MODIFY COLUMN create_time bigint DEFAULT 0 NOT NULL COMMENT '创建时间';

ALTER TABLE scm.cms_res_cat ADD unit_id bigint DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_res_cat CHANGE unit_id unit_id bigint DEFAULT 0 NOT NULL AFTER id;

ALTER TABLE scm.cms_doc_article CHANGE public_types visible TINYINT DEFAULT 0 NOT NULL;
ALTER TABLE scm.cms_doc_article MODIFY COLUMN visible TINYINT DEFAULT 0 NOT NULL;

ALTER TABLE scm.cms_doc_article ADD summary varchar(1024) NOT NULL;
ALTER TABLE scm.cms_doc_article CHANGE summary summary varchar(1024) NOT NULL AFTER visible;
