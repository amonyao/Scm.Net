using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Gtd.Detail.Dvo
{
    public class SearchRequest : ScmSearchPageRequest
    {
        public long header_id { get; set; }

        public GtdHandleEnum handle { get; set; }
    }
}
