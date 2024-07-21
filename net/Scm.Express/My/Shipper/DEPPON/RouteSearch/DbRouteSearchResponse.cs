using Com.Scm.Express.RouteSearch;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.DEPPON.RouteSearch
{
    public class DbRouteSearchResponse : DbResponse
    {
        /// <summary>
        /// 唯一请求编码
        /// </summary>
        public string uniquerRequestNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbTraceResult responseParam { get; set; }

        public void ToRespnose(RouteSearchResponse response)
        {
            if (responseParam != null)
            {
                var list = new List<RouteSearchResult>();
                foreach (var traceItem in responseParam.trace_list)
                {
                    var item = new RouteSearchResult();
                    item.mail_no = responseParam.tracking_number;
                    item.opt_time = traceItem.time;
                    item.opt_code = traceItem.status;
                    item.remark = traceItem.description;
                    item.opt_unit_name = traceItem.site;
                    list.Add(item);
                }
                response.Append(responseParam.tracking_number, list);
            }
        }
    }

    public class DbTraceResult
    {
        /// <summary>
        /// 德邦运单号
        /// </summary>
        public string tracking_number { get; set; }
        /// <summary>
        /// 轨迹列表
        /// </summary>
        public List<DbTraceList> trace_list { get; set; }
    }

    public class DbTraceList
    {
        /// <summary>
        /// 操作城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 轨迹描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// site
        /// </summary>
        public string site { get; set; }
        /// <summary>
        /// 订单状态
        /// GOT：开单；DEPARTURE：出站；ARRIVAL：进站；ADVANCE_DELIVERY ：预派送；SENT_SCAN：派送；ERROR：滞留,延时派送；FAILED：客户拒签/运单作废；SIGNED：签收；BACK_SIGNED：退回件签收；OPERATETRACK：转寄；STA_INBOUND：快递员入柜；STA_SIGN：用户提货（快递柜签收）；
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string time { get; set; }
    }
}
