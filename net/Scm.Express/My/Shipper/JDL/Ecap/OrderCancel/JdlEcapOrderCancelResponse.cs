namespace Com.Scm.Express.My.Shipper.JDL.Ecap.OrderCancel
{
    public class JdlEcapOrderCancelResponse : JdlEcapResponse<JdlEcapOrderCancelResult>
    {
    }
    public class JdlEcapOrderCancelResult
    {
        /// <summary>
        /// 京东物流订单号
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 京东物流运单号
        /// </summary>
        public string waybillCode { get; set; }
        /// <summary>
        /// 取消结果，枚举值：0 - 取消成功；1 - 拦截成功； 2 - 取消失败；3 - 拦截失败
        /// </summary>
        public int resultType { get; set; }
    }
}
