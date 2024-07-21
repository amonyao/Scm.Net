namespace Com.Scm.Express.My.Shipper.YTO.OrderFilter
{
    public class YtoOrderFilterResponse : YtoResponse
    {
        public OrderFilterResult data { get; set; }
    }

    public class OrderFilterResult
    {
        /// <summary>
        /// 查询响应编码 true-成功;false-失败
        /// </summary>
        public string success { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 网点是否停止派送 Y-停止派件，N-正常派件
        /// </summary>
        public string orgStopSend { get; set; }
        /// <summary>
        /// 网点编码
        /// </summary>
        public string recipientOrgCode { get; set; }
        /// <summary>
        /// 网点名称
        /// </summary>
        public string recipientOrgName { get; set; }
        /// <summary>
        /// 网点是否支持到付 Y-不支持到付，N-支持到付
        /// </summary>
        public string stopFreightCollect { get; set; }
        /// <summary>
        /// 网点是否支持代收款 Y-不支持代收款，N-支持
        /// </summary>
        public string canAgencyFund { get; set; }
        /// <summary>
        /// 请求标识
        /// </summary>
        public string traceId { get; set; }
        /// <summary>
        /// 标准价格
        /// </summary>
        public string standPrice { get; set; }
        /// <summary>
        /// 超重价格
        /// </summary>
        public string overWeightPrice { get; set; }
        /// <summary>
        /// 	最终价格
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 	到付价格
        /// </summary>
        public string collectAmount { get; set; }
    }
}
