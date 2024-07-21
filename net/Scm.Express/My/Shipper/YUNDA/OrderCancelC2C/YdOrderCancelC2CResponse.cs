namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCancelC2C
{
    public class YdOrderCancelC2CResponse : YdResponse<OrderCancelResult>
    {
    }

    public class OrderCancelResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 原样返回字段
        /// </summary>
        public string backparam { get; set; }
    }
}
