namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCreateC2C
{
    public class YdOrderCreateC2CResponse : YdResponse<OrderCreateResult>
    {
    }

    public class OrderCreateResult
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
