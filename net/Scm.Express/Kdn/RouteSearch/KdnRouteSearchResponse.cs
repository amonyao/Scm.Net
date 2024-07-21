using System.Collections.Generic;

namespace Com.Scm.Express.Kdn.RouteSearch
{
    public class KdnRouteSearchResponse : KdnResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string EBusinessID { get; set; }
        /// <summary>
        /// 快递公司编码
        /// </summary>
        public string ShipperCode { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string LogisticCode { get; set; }
        /// <summary>
        /// 成功与否
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 普通物流状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 轨迹
        /// </summary>
        public List<KdnRouteSearchResult> Traces { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
    }

    public class KdnRouteSearchResult
    {
        /// <summary>
        /// 轨迹发生时间
        /// </summary>
        public string AcceptTime { get; set; }
        /// <summary>
        /// 轨迹描述
        /// </summary>
        public string AcceptStation { get; set; }
    }
}
