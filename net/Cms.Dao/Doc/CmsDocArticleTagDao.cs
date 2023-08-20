using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("cms_doc_article_tag")]
    public class CmsDocArticleTagDao : ScmUnitDataDao
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