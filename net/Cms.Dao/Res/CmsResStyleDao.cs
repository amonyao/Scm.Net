using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_res_style")]
    public class CmsResStyleDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int types { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(128)]
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(2048)]
        public string style { get; set; }
    }
}