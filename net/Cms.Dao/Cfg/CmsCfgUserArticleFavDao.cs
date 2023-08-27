using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_cfg_user_article_fav")]
    public class CmsCfgUserArticleFavDao : ScmDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long article_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;
    }
}