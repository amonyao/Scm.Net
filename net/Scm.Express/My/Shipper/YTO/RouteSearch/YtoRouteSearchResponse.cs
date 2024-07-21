using Com.Scm.Express.RouteSearch;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YTO.RouteSearch
{
    public class YtoRouteSearchResponse : YtoResponse
    {
        public Dictionary<string, List<YtoRouteSearchResult>> map { get; set; }

        public void ToResponse(RouteSearchResponse response)
        {
            if (map != null)
            {
                foreach (var key in map.Keys)
                {
                    var val = map[key];
                    var list = new List<RouteSearchResult>();
                    foreach (var item in val)
                    {
                        var result = new RouteSearchResult();
                        result.opt_time = item.upload_Time;
                        result.remark = item.processInfo;
                        list.Add(result);
                    }
                    response.Append(key, list);
                }
            }
        }
    }

    public class YtoRouteSearchResult
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybill_No { get; set; }
        /// <summary>
        /// 走件产生时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string upload_Time { get; set; }
        /// <summary>
        /// 物流状态，固定为：GOT 已收件;ARRIVAL 已收入;DEPARTURE 已发出;PACKAGE 已打包;SENT_SCAN 派件;INBOUND 自提柜入柜;SIGNED 签收成功;FAILED 签收失败;FORWARDING 转寄;TMS_RETURN 退回;
        /// </summary>
        public string infoContent { get; set; }
        /// <summary>
        /// 物流信息
        /// </summary>
        public string processInfo { get; set; }
    }
}
