using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fes.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CatDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(1024)]
        public string path { get; set; }

        /// <summary>
        /// 上级类别
        /// </summary>
        [Required]
        public long pid { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 图像数量
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}