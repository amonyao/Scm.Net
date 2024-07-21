namespace Com.Scm.Express.My.Shipper.SF.Scs.Model
{
    public class OrderService
    {
        /// <summary>
        /// 月结卡号
        /// </summary>
        public string monthlyAccount { get; set; }
        /// <summary>
        /// 付款方式编码
        /// </summary>
        public string paymentTypeCode { get; set; }
        /// <summary>
        /// 增值服务编码
        /// </summary>
        public string serviceCode { get; set; }
        /// <summary>
        /// 费用金额
        /// </summary>
        public string feeAmount { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string extensionInfo { get; set; }
    }
}
