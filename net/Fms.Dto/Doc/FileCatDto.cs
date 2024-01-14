using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fes.Doc
{
    /// <summary>
    /// 图像软链
    /// </summary>
    public class FileCatDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string codes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string namec { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 图像
        /// </summary>
        [Required]
        public long file_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public char salt { get; set; }
    }
}