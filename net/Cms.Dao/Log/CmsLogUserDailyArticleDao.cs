using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Log
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_log_user_daily_article")]
    public class CmsLogUserDailyArticleDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long article_id { get; set; }
    }
}