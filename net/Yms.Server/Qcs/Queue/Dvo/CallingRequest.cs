using Com.Scm.Dvo;

namespace Com.Scm.Yms.Qcs.Queue.Dvo
{
    public class CallingRequest : ScmRequest
    {
        public long id { get; set; }
        public CallingDirEnums dir { get; set; }
    }

    public enum CallingDirEnums
    {
        None = 0,
        Backward = 1,
        Repeat = 2,
        Forward = 3,
    }
}
