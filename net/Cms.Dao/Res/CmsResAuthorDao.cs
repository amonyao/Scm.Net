using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Res
{
    /// <summary>
    /// 作者
    /// </summary>
    [SugarTable("cms_res_author")]
    public class CmsResAuthorDao : ScmDataDao
    {
        /// <summary>
        /// 国别
        /// </summary>
        [Required]
        public long nation_id { get; set; }

        /// <summary>
        /// 朝代
        /// </summary>
        [Required]
        public long dynasty_id { get; set; }

        /// <summary>
        /// 显示排序
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

        /// <summary>
        /// Name
        /// </summary>
        [StringLength(256)]
        public string namee { get; set; }
    }
}