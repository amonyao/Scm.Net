using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Article.Dvo
{
    public class DailyResponse : ScmResponse
    {
        public string dates { get; set; }

        public CmsDocArticleDvo article { get; set; }
    }
}
