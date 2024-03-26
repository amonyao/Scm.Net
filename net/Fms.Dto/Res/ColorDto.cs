using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class ColorDto : ScmDataDto
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