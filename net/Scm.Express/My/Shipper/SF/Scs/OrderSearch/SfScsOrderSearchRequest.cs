using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.SF.Scs.OrderSearch
{
    public class SfScsOrderSearchRequest : SfScsRequest
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string erpOrder { get; set; }

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
            return "SCS_RECE_QUERY_ORDER_INFO";
        }
    }
}
