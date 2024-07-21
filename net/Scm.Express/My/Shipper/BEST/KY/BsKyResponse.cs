namespace Com.Scm.Express.My.Shipper.BEST.KY
{
    public class BsKyResponse : BsResponse
    {
        public string resultInfo { get; set; }
        public string resultCode { get; set; }
        public bool result { get; set; }

        public override bool IsSuccess()
        {
            return result;
        }

        public override string GetMessage()
        {
            return resultInfo;
        }
    }
}
