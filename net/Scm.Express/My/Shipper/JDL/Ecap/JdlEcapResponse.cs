namespace Com.Scm.Express.My.Shipper.JDL.Ecap
{
    public class JdlEcapResponse<T> : JdlResponse
    {
        public T data { get; set; }

        public string requestId { get; set; }
        public long mills { get; set; }
        public bool success { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
        public string subMsg { get; set; }

        public override bool IsSuccess()
        {
            return success;
        }

        public override string GetMessage()
        {
            return msg;
        }
    }
}
