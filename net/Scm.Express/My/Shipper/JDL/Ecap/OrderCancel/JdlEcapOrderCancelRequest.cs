namespace Com.Scm.Express.My.Shipper.JDL.Ecap.OrderCancel
{
    public class JdlEcapOrderCancelRequest : JdlEcapRequest
    {
        /// <summary>
        /// 	京东物流运单号，运单号在下单接口中已返回，同订单号必填其一
        /// </summary>
        public string waybillCode { get; set; }
        /// <summary>
        /// 	京东物流订单号，订单号在下单接口中已返回，与京东运单号必填其一
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 	下单来源：0-c2c；1-b2c；2-c2b；与下单接口入参保持一致
        /// </summary>
        public int orderOrigin { get; set; }
        /// <summary>
        /// 	客户编码，orderOrigin=1 或者orderOrigin= 2时必填；与下单时使用的客户编码保持一致
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 	取消原因，商家自定义，字段长度1-30
        /// </summary>
        public string cancelReason { get; set; }
        /// <summary>
        /// 	取消原因编码；枚举值：1-用户发起取消；2-超时未支付
        /// </summary>
        public string cancelReasonCode { get; set; }
        /// <summary>
        /// 	枚举值：orderOrigin=0时此处传0；orderOrigin=1或2时此处传1。
        /// </summary>
        public int cancelType { get; set; }

        public override string GetDomain()
        {
            return "ECAP";
        }

        public override string GetPath()
        {
            return "/ecap/v1/orders/cancel";
        }
    }
}
