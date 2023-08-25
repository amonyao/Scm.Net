using Com.Scm.Cms.Enums;
using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    public class CmsDocLiteraDto : ScmDataDto
    {
        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public VisibleEnum visible { get; set; } = 0;

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string content { get; set; }

        /// <summary>
        /// 是否批量添加
        /// </summary>
        public bool batch { get; set; }

        /// <summary>
        /// 换行数量
        /// </summary>
        public int blank { get; set; }
    }
}
