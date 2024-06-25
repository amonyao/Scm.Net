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
        /// 
        /// </summary>
        [StringLength(32)]
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public OspVerEnum ver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 
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