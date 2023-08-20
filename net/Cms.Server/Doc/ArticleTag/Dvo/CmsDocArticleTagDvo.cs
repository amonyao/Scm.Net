using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsDocArticleTagDvo : ScmDataDvo
    {
        /// <summary>
        /// 文章
        /// </summary>
        public long article_id { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public long tag_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; } = 0;
    }
}