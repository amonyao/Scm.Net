using System.Collections.Generic;

namespace Com.Scm.Express.JdExpress.OrderCreate
{
    public class JdOrderCreateResponse : JdResponse<WaybillResultDTO>
    {
    }

    public class WaybillResultDTO
    {
        /// <summary>
        /// 运单号列表
        /// </summary>
        public List<string> waybillCodeList { get; set; }
        /// <summary>
        /// 平台订单号
        /// </summary>
        public string platformOrderNo { get; set; }
        /// <summary>
        /// 京东包裹号
        /// </summary>
        public string packageNo { get; set; }
        /// <summary>
        /// 承运商编码
        /// </summary>
        public string providerCode { get; set; }
        /// <summary>
        /// 承运商名称
        /// </summary>
        public string providerName { get; set; }
        /// <summary>
        /// 青龙站点ID
        /// </summary>
        public string siteId { get; set; }
        /// <summary>
        /// 青龙站点名称
        /// </summary>
        public string siteName { get; set; }
    }
}
