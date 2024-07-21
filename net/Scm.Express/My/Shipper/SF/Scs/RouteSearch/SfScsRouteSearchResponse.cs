namespace Com.Scm.Express.My.Shipper.SF.Scs.RouteSearch
{
    public class SfScsRouteSearchResponse
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string routeId { get; set; }
        /// <summary>
        /// 巴枪扫描时间
        /// </summary>
        public string barScanTm { get; set; }
        /// <summary>
        /// 网点对外名称
        /// </summary>
        public string outsideName { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string distName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string opCode { get; set; }
        /// <summary>
        /// 官网备注描述
        /// </summary>
        public string owsRemark { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybillNo { get; set; }
        /// <summary>
        /// SF生成系统订单号
        /// </summary>
        public string sfOrderNo { get; set; }
        /// <summary>
        /// 客户erpOrder订单号
        /// </summary>
        public string erpOrder { get; set; }
    }
}
