using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Res
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("fes_res_cat_color")]
    public class CatColorDao : ScmDataDao
    {
        /// <summary>
        /// 类别
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [Required]
        public long color_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}