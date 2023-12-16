using Com.Scm.Dvo;

namespace Com.Scm.Yms.Qcs.Queue.Dvo
{
    public class SearchRequest : ScmSearchPageRequest
    {
        public long header_id { get; set; }
        public long detail_id { get; set; }

        public QcsQueueHandleEnums handle { get; set; }
    }
}
