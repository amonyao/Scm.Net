using Com.Scm.Dao;
using SqlSugar;

namespace Com.Scm.Dr.Web
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("scm_dr_web_daily")]
    public class ScmDrWebDailyDao : ScmDataDao
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string day { get; set; }

        /// <summary>
        /// 页面访问量
        /// </summary>
        public int pv { get; set; }
        /// <summary>
        /// 用户访问量
        /// </summary>
        public int uv { get; set; }
    }
}