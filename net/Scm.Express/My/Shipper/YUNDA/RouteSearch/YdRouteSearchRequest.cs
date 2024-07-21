using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.YUNDA.RouteSearch
{
    public class YdRouteSearchRequest : YdRequest
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string mailno { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.yundaex.com/openapi/outer/logictis/query";
            }
            return "https://u-openapi.yundasys.com/openapi/outer/logictis/query";
        }
    }
}
