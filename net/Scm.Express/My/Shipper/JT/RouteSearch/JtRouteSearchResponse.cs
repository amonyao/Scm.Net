using Com.Scm.Express.RouteSearch;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.JT.RouteSearch
{
    public class JtRouteSearchResponse : JtResponse<List<JtRouteSearchResult>>
    {
        public void ToResponse(RouteSearchResponse response)
        {
            foreach (var item in data)
            {
                var list = new List<RouteSearchResult>();
                foreach (var detail in item.details)
                {
                    var result = new RouteSearchResult();
                    result.opt_time = detail.scanTime;
                    result.remark = detail.desc;

                    result.opt_unit_code = "" + detail.scanNetworkId;
                    result.opt_unit_name = detail.scanNetworkName;
                    result.opt_unit_phone = detail.scanNetworkContact;

                    result.opt_user_code = detail.staffName;
                    result.opt_user_phone = detail.staffContact;
                    list.Add(result);
                }
                response.Append(item.billCode, list);
            }
        }
    }

    public class JtRouteSearchResult
    {
        public string billCode { get; set; }
        public List<JtRouteSearchResultDetail> details { get; set; }
    }

    public class JtRouteSearchResultDetail
    {
        /// <summary>
        /// 扫描时间
        /// </summary>
        public string scanTime { get; set; }
        /// <summary>
        /// 轨迹描述
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 扫描类型
        /// 1、快件揽收
        /// 2、入仓扫描（停用）
        /// 3、发件扫描
        /// 4、到件扫描
        /// 5、出仓扫描
        /// 6、入库扫描
        /// 7、代理点收入扫描
        /// 8、快件取出扫描
        /// 9、出库扫描
        /// 10、快件签收
        /// 11、问题件扫描
        /// </summary>
        public string scanType { get; set; }
        /// <summary>
        /// A1、客户取消寄件
        /// A2、客户拒收
        /// </summary>
        public string problemType { get; set; }
        /// <summary>
        /// 扫描网点名称
        /// </summary>
        public string scanNetworkName { get; set; }
        /// <summary>
        /// 扫描网点ID
        /// </summary>
        public int scanNetworkId { get; set; }
        /// <summary>
        /// 业务员姓名
        /// </summary>
        public string staffName { get; set; }
        /// <summary>
        /// 业务员联系方式
        /// </summary>
        public string staffContact { get; set; }
        /// <summary>
        /// 扫描网点联系方式
        /// </summary>
        public string scanNetworkContact { get; set; }
        /// <summary>
        /// 扫描网点省份
        /// </summary>
        public string scanNetworkProvince { get; set; }
        /// <summary>
        /// 扫描网点城市
        /// </summary>
        public string scanNetworkCity { get; set; }
        /// <summary>
        /// 扫描网点区/县
        /// </summary>
        public string scanNetworkArea { get; set; }
        /// <summary>
        /// 上一站(到件)或下一站名称(发件)
        /// </summary>
        public string nextStopName { get; set; }
        /// <summary>
        /// 	下一站省份（发件扫描类型时提供）
        /// </summary>
        public string nextNetworkProvinceName { get; set; }
        /// <summary>
        /// 	下一站城市（发件扫描类型时提供）
        /// </summary>
        public string nextNetworkCityName { get; set; }
        /// <summary>
        /// 	下一站区/县（发件扫描类型时提供）
        /// </summary>
        public string nextNetworkAreaName { get; set; }
    }
}
