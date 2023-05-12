using Com.Scm.Dvo;

namespace Com.Scm.Samples.Demo.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchRequest : ScmSearchPageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public long option_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string create_time { get; set; }
    }
}
