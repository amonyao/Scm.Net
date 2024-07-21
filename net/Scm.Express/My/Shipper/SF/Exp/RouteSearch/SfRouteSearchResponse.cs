using Com.Scm.Express.RouteSearch;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Exp.RouteSearch
{
    public class SfRouteSearchResponse
    {
        public List<RouteResult> routeResps { get; set; }

        public void ToResponse(RouteSearchResponse response)
        {
            if (routeResps != null)
            {
                foreach (var routeResp in routeResps)
                {
                    var list = new List<RouteSearchResult>();
                    foreach (var route in routeResp.routes)
                    {
                        var item = new RouteSearchResult();
                        list.Add(item);

                        item.mail_no = routeResp.mailNo;
                        item.opt_time = route.acceptTime;
                        item.remark = route.remark;
                    }
                    response.Append(routeResp.mailNo, list);
                }
            }
        }

        public bool IsCollect()
        {
            if (routeResps == null)
            {
                return false;
            }

            return routeResps.Count > 0;
        }
    }

    public class RouteResult
    {
        public string mailNo { get; set; }

        public List<RouteInfo> routes { get; set; }
    }

    public class RouteInfo
    {
        /// <summary>
        /// 路由节点发生的时间，格式：YYYY-MM-DD HH24:MM:SS，示例：2012-7-30 09:30:00
        /// </summary>
        public string acceptTime { get; set; }
        /// <summary>
        /// 路由节点发生的地点
        /// </summary>
        public string acceptAddress { get; set; }
        /// <summary>
        /// 路由节点具体描述
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 路由节点操作码
        /// </summary>
        public string opcode { get; set; }
    }
}
