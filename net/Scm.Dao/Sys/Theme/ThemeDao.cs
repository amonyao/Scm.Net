using Com.Scm.Dao;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Sys.Theme
{
    /// <summary>
    /// 
    /// </summary>
    [SqlSugar.SugarTable("scm_sys_theme")]
    public class ThemeDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(1024)]
        public string theme { get; set; }
    }
}
