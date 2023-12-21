using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Blog.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchRequest : ScmSearchPageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public long cat_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int types { get; set; }
    }
}
