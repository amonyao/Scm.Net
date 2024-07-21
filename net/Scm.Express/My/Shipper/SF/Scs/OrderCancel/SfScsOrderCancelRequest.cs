using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.SF.Scs.OrderCancel
{
    public class SfScsOrderCancelRequest : SfScsRequest
    {
        /// <summary>
        /// 运输订单号
        /// </summary>
        public string erpOrder { get; set; }
        /// <summary>
        /// SF生成订单号
        /// </summary>
        public string sfOrderNo { get; set; }

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
            return "SCS_RECE_CANCEL_ORDER";
        }
    }
}
