using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.ZTO.RouteSearch.V2
{
    public class ZtoRouteSearchRequest : ZtoRequest
    {
        public string billCode { get; set; }
        public string mobilePhone { get; set; }

        public override string GetServiceUrl()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://japi.zto.com/zto.merchant.waybill.track.query";
            }
            return "https://japi-test.zto.com/zto.merchant.waybill.track.query";
        }
    }
}
