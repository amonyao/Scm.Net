using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsDocArticleTagDto : ScmDataDto
    {
        /// <summary>
        /// 文章
        /// </summary>
        [Required]
        public long article_id { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [Required]
        public long tag_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; } = 0;
    }
}