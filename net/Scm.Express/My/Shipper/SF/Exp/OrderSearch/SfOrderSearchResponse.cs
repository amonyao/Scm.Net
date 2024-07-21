using Com.Scm.Express.My.Shipper.SF.Exp.Model;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderSearch
{
    public class SfOrderSearchResponse
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 原寄地区域代码，可用 于顺丰电子运单标签打印
        /// </summary>
        public string originCode { get; set; }

        /// <summary>
        /// 目的地区域代码，可用 于顺丰电子运单标签打印
        /// </summary>
        public string destCode { get; set; }

        /// <summary>
        /// 筛单结果： 1：人工确认 2：可收派 3：不可以收派
        /// </summary>
        public string filterResult { get; set; }

        /// <summary>
        /// 返回信息扩展属性
        /// </summary>
        public List<ExtraInfo> returnExtraInfoList { get; set; }

        /// <summary>
        /// 顺丰运单号
        /// </summary>
        public List<WaybillNoInfo> waybillNoInfoList { get; set; }

        /// <summary>
        /// 路由标签数据
        /// </summary>
        public List<RouteLabelInfo> routeLabelInfo { get; set; }
    }
}