using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.STO.RouteSearch
{
    public class StoRouteSearchRequest : StoRequest
    {
        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }

        /// <summary>
        /// 运单号集合
        /// </summary>
        public List<string> waybillNoList { get; set; }

        public override string GetApiName()
        {
            return "STO_TRACE_QUERY_COMMON";
        }

        public override string GetToAppKey()
        {
            return "sto_trace_query";
        }

        public override string GetToCode()
        {
            return "sto_trace_query";
        }
    }
}
