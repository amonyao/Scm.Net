﻿using Com.Scm.Dao;
using Com.Scm.Log;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Res.Sms
{
    /// <summary>
    /// 消息模板
    /// </summary>
    [SugarTable("scm_res_sms")]
    public class SmsDao : ScmDataDao
    {
        /// <summary>
        /// 模板类型
        /// </summary>
        public SmsTypesEnum types { get; set; }

        /// <summary>
        /// 模板代码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [StringLength(64)]
        public string namec { get; set; }

        /// <summary>
        /// 标题模板
        /// </summary>
        [StringLength(128)]
        public string head { get; set; }

        /// <summary>
        /// 内容模板
        /// </summary>
        [StringLength(512)]
        public string body { get; set; }

        /// <summary>
        /// 声明模板
        /// </summary>
        [StringLength(128)]
        public string foot { get; set; }

        /// <summary>
        /// 文件模板
        /// </summary>
        [StringLength(64)]
        public string file { get; set; }
    }
}
