using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Gtd.Dvo
{
    public class SearchRequest : ScmSearchPageRequest
    {
        public GtdHandleEnum handle { get; set; }
    }
}
