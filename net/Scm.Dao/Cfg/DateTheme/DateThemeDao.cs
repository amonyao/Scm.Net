using Com.Scm.Dao;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cfg.DateTheme
{
    /// <summary>
    /// 每日主题
    /// </summary>
    [SqlSugar.SugarTable("scm_cfg_date_theme")]
    public class DateThemeDao : ScmDataDao
    {
        /// <summary>
        /// 日期。
        /// 格式：yyyy-MM-dd
        /// </summary>
        [StringLength(16)]
        public string dates { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public long theme_id { get; set; }
    }
}
