using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.YTO.RouteSearch
{
    public class YtoRouteSearchRequest : YtoRequest
    {
        /// <summary>
        /// 圆通物流运单号，一次只能查询一个单号。
        /// </summary>
        public string number { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "http://openapi.yto.net.cn/service/waybill_query/v1/PFUYji";
            }
            return "https://openuat.yto56.com.cn:6443/open/track_query_adapter/v1/PFUYji/TEST";
        }

        public override string GetServiceCode()
        {
            return "track_query_adapter";
        }

        public override string GetServiceVer()
        {
            return "v1";
        }

        public override string GetPartnerCode()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "lPYgWm";
            }
            return "TEST";
        }

        public override string GetPartnerPass()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "mE7vBe";
            }
            return "123456";
        }
    }
}
