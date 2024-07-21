namespace Com.Scm.Express.Kdn.RouteSearch
{
    public class KdnRouteSearchRequest : KdnRequest
    {
        /// <summary>
        /// 快递公司编码
        /// </summary>
        public string ShipperCode { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string LogisticCode { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }

        public override string GetRequestType()
        {
            return "1002";
        }

        public override string GetServicePath()
        {
            return "https://api.kdniao.com/Ebusiness/EbusinessOrderHandle.aspx";
        }
    }
}
