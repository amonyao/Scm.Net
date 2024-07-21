using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.RouteSearch
{
    public class YdRouteSearchResponse : YdResponse<RouteSearchResult>
    {
    }

    public class RouteSearchResult
    {
        /// <summary>
        /// 响应状态
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string mailno { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 轨迹参数
        /// </summary>
        public List<RouteSearchStep> steps { get; set; }
    }

    public class RouteSearchStep
    {
        /// <summary>
        /// 轨迹产生时间 格式 yyyy-MM-dd HH : mm : ss	
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 当前状态 见下面节点状态status说明	
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 节点状态 见下面节点状态action说明	
        /// </summary>
        public string action { get; set; }
        /// <summary>
        /// 当前城市	
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 下级站点城市	
        /// </summary>
        public string next_city { get; set; }
        /// <summary>
        /// 轨迹发生站点编码	
        /// </summary>
        public string station { get; set; }
        /// <summary>
        /// 轨迹发生站点名称	
        /// </summary>
        public string station_name { get; set; }
        /// <summary>
        /// 轨迹发生站点类型1是网点，2是分拨中心	
        /// </summary>
        public string station_type { get; set; }
        /// <summary>
        /// 下级站点编码	
        /// </summary>
        public string next { get; set; }
        /// <summary>
        /// 下级站点名称	
        /// </summary>
        public string next_name { get; set; }
        /// <summary>
        /// 下级站点类型 1是网点，2是分拨中心	
        /// </summary>
        public string next_type { get; set; }
        /// <summary>
        /// 轨迹描述信息
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 快递员电话		
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public string signer { get; set; }
    }
}
