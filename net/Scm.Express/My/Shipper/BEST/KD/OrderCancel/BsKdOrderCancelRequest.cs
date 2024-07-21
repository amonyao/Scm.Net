namespace Com.Scm.Express.My.Shipper.BEST.KD.OrderCancel
{
    public class BsKdOrderCancelRequest : BsKdRequest
    {
        public string txLogisticID { get; set; }
        public string reason { get; set; }

        public override string GetServiceType()
        {
            return "KD_CANCEL_ORDER_NOTIFY";
        }
    }
}
