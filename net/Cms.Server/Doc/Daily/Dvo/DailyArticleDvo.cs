using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Daily.Dvo
{
    public class DailyArticleDvo : ScmDataDvo
    {
        public string dates { get; set; }

        public CmsDocArticleDvo article { get; set; }
    }
}
