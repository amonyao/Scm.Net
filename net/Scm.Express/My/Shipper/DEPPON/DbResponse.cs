namespace Com.Scm.Express.My.Shipper.DEPPON
{
    public class DbResponse
    {
        public string result { get; set; }
        public string reason { get; set; }
        public string resultCode { get; set; }

        public virtual bool IsSuccess()
        {
            return "true" == result;
        }

        public virtual string GetMessage()
        {
            return reason;
        }
    }
}
