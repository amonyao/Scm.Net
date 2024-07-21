namespace Com.Scm.Express.My.Shipper.BEST.KY.OrderCancel
{
    public class BsKyOrderCancelRequest : BsKyRequest
    {
        public string logisticID { get; set; }
        public string logisticCompanyID { get; set; }
        public string gmtCancel { get; set; }
        public string comments { get; set; }

        public override string GetServiceType()
        {
            return "KY_CANCEL_ORDER_NOTIFY";
        }
    }
}
