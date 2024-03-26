using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Res
{
    /// <summary>
    /// 国别
    /// </summary>
    [SugarTable("cms_res_nation")]
    public class CmsResNationDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }
    }
}