using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 图片数据
    /// </summary>
    public class ImageDto : FileDto
    {
        /// <summary>
        /// 宽
        /// </summary>
        [Required]
        public int width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        [Required]
        public int height { get; set; }

        /// <summary>
        /// EXIF信息
        /// </summary>
        [StringLength(1024)]
        public string exif { get; set; }
    }
}