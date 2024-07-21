namespace Com.Scm.Express.My.Shipper.JDL.Ecap.RouteSearch
{
    public class JdlEcapRouteSearchRequest : JdlEcapRequest
    {
        /// <summary>
        /// 京东物流运单号，运单号在下单接口中已返回，与物流订单号必填其一
        /// </summary>
        public string waybillCode { get; set; }
        /// <summary>
        /// 京东物流订单号，订单号在下单接口中已返回，与物流运单号必填其一
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 下单来源；枚举值：0-c2c；1-b2c；2-c2b；与下单时的入参保持一致；详细说明查看：https://cloud.jdl.com/#/open-business-document/access-guide/267/54152
        /// </summary>
        public int orderOrigin { get; set; }
        /// <summary>
        /// 客户编码；orderOrigin为 1 时必填，与下单时使用的客户编码保持一致
        /// </summary>
        public string customerCode { get; set; }

        public override string GetPath()
        {
            return "/ecap/v1/orders/trace/query";
        }
    }
}
