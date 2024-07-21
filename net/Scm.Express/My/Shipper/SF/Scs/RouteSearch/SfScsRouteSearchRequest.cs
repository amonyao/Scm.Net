using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.SF.Scs.RouteSearch
{
    public class SfScsRouteSearchRequest : SfScsRequest
    {
        /// <summary>
        /// 客户erpOrder订单号
        /// </summary>
        public string erpOrder { get; set; }

        /// <summary>
        /// SF生成系统订单号
        /// </summary>
        public string sfOrderNo { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string waybillNo { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://sfapi.sf-express.com/std/service";
            }
            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "SCS_RECE_QUERY_ROUTE";
        }
    }
}
