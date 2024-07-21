using Com.Scm.Express.OrderCancel;

namespace Com.Scm.Express.My.Shipper.JT.OrderCancel
{
    public class JtOrderCancelResponse : JtResponse<JtOrderCancelResult>
    {
        public void ToResponse(OrderCancelResponse response)
        {

        }
    }

    public class JtOrderCancelResult
    {
        public string billCode { get; set; }
        public string txlogisticId { get; set; }
    }
}
