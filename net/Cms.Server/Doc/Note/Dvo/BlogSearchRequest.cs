using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Note.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class BlogSearchRequest : ScmSearchPageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public long cat_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SortTypeEnums types { get; set; }
    }

    public enum SortTypeEnums
    {
        None = 0,
        /// <summary>
        /// 推荐
        /// </summary>
        BySuggest = 1,
        /// <summary>
        /// 最新
        /// </summary>
        ByCreateTime = 2,
        /// <summary>
        /// 最热
        /// </summary>
        ByQty = 3,
    }
}
