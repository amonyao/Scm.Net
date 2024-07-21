namespace Com.Scm.Express.My.Shipper.BEST.KY.OrderFilter
{
    public class BsKyOrderFilterRequest : BsKyRequest
    {
        public string address { get; set; }

        public override string GetServiceType()
        {
            return "KY_ADDRESS_REACHABLE_QUERY";
        }
    }
}
