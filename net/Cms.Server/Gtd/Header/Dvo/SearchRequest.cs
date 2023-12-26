using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Gtd.Header.Dvo
{
    public class SearchRequest : ScmSearchPageRequest
    {
        public long cat_id { get; set; }

        public GtdHandleEnum handle { get; set; }
    }
}
