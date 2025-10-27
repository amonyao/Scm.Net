using Com.Scm.Api;

namespace Com.Scm
{
    public class ScmExportResponse : ScmApiResponse
    {
        public long task { get; set; }
        public string file { get; set; }
    }
}
