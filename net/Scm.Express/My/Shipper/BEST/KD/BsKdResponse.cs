namespace Com.Scm.Express.My.Shipper.BEST.KD
{
    public class BsKdResponse : BsResponse
    {
        public bool result { get; set; }
        public string errorCode { get; set; }
        public string errorDescription { get; set; }

        public override bool IsSuccess()
        {
            return result;
        }

        public override string GetMessage()
        {
            return errorDescription;
        }
    }
}
