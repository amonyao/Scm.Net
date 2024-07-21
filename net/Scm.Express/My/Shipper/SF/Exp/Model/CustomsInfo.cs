namespace Com.Scm.Express.My.Shipper.SF.Exp.Model
{
    /// <summary>
    /// 海关信息
    /// </summary>
    public class CustomsInfo
    {
        /// <summary>
        /// 客户订单货物总声明价值， 包含子母件，精确到小数点 后3位。如果是跨境件，则必填
        /// </summary>
        public string declaredValue { get; set; }
        /// <summary>
        /// 货物声明价值币别，跨境 件报关需要填写 参照附录币别代码附件
        /// </summary>
        public string declaredValueCurrency { get; set; }
        /// <summary>
        /// 报关批次
        /// </summary>
        public string customsBatchs { get; set; }
        /// <summary>
        /// 税金付款方式，支持以下值： 1:寄付 2：到付
        /// </summary>
        public string taxPayMethod { get; set; }
        /// <summary>
        /// 税金结算账号
        /// </summary>
        public string taxSettleAccounts { get; set; }
        /// <summary>
        /// 支付工具
        /// </summary>
        public string paymentTool { get; set; }
        /// <summary>
        /// 支付号码
        /// </summary>
        public string paymentNumber { get; set; }
        /// <summary>
        /// 客户订单下单人姓名
        /// </summary>
        public string orderName { get; set; }
        /// <summary>
        /// 客户订单下单人证件类型
        /// </summary>
        public string orderCertType { get; set; }
        /// <summary>
        /// 客户订单下单人证件号
        /// </summary>
        public string orderCertNo { get; set; }
        /// <summary>
        /// 税款
        /// </summary>
        public string tax { get; set; }
    }
}
