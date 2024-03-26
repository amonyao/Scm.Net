using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Res
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("fes_res_color")]
    public class ColorDao : ScmDataDao
    {
        /// <summary>
        /// 颜色
        /// </summary>
        [Required]
        public int color { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}