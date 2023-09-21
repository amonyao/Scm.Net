using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Daily.Dvo
{
    public class CmsLogUserDailyArticleDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long article_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 冗余
        /// </summary>
        public string title { get; set; }
    }
}
