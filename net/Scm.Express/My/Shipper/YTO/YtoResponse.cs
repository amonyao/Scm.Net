namespace Com.Scm.Express.My.Shipper.YTO
{
    public class YtoResponse
    {
        public string success { get; set; }
        public string code { get; set; }
        public string message { get; set; }

        public virtual bool IsSuccess()
        {
            return "true" == success;
        }

        public virtual string GetMessage()
        {
            return message;
        }
    }
}
