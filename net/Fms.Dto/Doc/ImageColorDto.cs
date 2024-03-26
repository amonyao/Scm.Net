using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 图片颜色
    /// </summary>
    public class ImageColorDto : ScmDataDto
    {
        /// <summary>
        /// 图像
        /// </summary>
        [Required]
        public long file_id { get; set; }

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
    }
}