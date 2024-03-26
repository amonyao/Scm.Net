using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Res
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_res_tag")]
    public class CmsResTagDao : ScmUnitDataDao
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int types { get; set; } = 0;

        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(32)]
        public string content { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;
    }
}