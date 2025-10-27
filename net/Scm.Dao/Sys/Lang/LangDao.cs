﻿using Com.Scm.Dao;
using Com.Scm.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Sys.Lang
{
    [SqlSugar.SugarTable("scm_sys_lang")]
    public class LangDao : ScmDao
    {
        /// <summary>
        /// 语言编码
        /// </summary>
        [StringLength(8)]
        public string code { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary>
        [StringLength(32)]
        public string text { get; set; }
        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public ScmRowStatusEnum row_status { get; set; }
    }
}
