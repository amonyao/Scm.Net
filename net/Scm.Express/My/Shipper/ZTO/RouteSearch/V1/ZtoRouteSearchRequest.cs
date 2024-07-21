using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.ZTO.RouteSearch.V1
{
    public class ZtoRouteSearchRequest : ZtoRequest
    {
        public string billCode { get; set; }

        public override string GetServiceUrl()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://japi.zto.com/zto.open.getRouteInfo";
            }
            return "https://japi-test.zto.com/zto.open.getRouteInfo";
        }
    }
}
