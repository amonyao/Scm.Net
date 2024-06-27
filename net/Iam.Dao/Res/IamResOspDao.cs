using Com.Scm.Dao;
using Com.Scm.Enums;
using Com.Scm.OAuth.Web.Dto;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Res
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("iam_res_osp")]
    public class IamResOspDao : ScmDataDao
    {
        /// <summary>
        /// 服务代码
        /// </summary>
        [StringLength(32)]
        public string code { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [StringLength(32)]
        public string name { get; set; }

        /// <summary>
        /// 服务图标
        /// </summary>
        [StringLength(32)]
        public string icon { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        [StringLength(128)]
        public string url { get; set; }

        /// <summary>
        /// 协议版本
        /// </summary>
        [Required]
        public OspVerEnum ver { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 调用频次
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public ScmSystemEnum row_system { get; set; }
    }
}