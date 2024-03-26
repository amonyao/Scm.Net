using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class CatColorDto : ScmDataDto
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
        public int od { get; set; } = 0;

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