using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Res
{
    /// <summary>
    /// 类别
    /// </summary>
    public class CmsResCatDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public const long SYS_ID = 1000000000000000001L;

        /// <summary>
        /// 类目类型，同文章类型
        /// </summary>
        [Required]
        public int types { get; set; } = 0;

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [Required]
        public long pid { get; set; }

        /// <summary>
        /// 顶级ID
        /// </summary>
        [Required]
        public long tid { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;

        /// <summary>
        /// 样式
        /// </summary>
        [Required]
        public long style_id { get; set; }
    }
}