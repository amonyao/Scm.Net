using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.JT.RouteSearch
{
    public class JtRouteSearchRequest : JtRequest
    {
        public string billCodes { get; set; }

        public override string GetPath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.jtexpress.com.cn/webopenplatformapi/api/logistics/trace";
            }
            return "https://uat-openapi.jtexpress.com.cn/webopenplatformapi/api/logistics/trace?uuid=9c87b5e9c41d441ab06f31db51d79c15";
        }

        public void Append(string mailNo)
        {
            if (!string.IsNullOrWhiteSpace(billCodes))
            {
                billCodes += ",";
            }
            billCodes += mailNo;
        }
    }
}
