using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.SF.Exp.WaybillCheck
{
    public class SfExpWaybillCheckRequest : SfExpRequest
    {
        /// <summary>
        /// 运单号 （支持12位或者15位运单号）
        /// </summary>
        public string waybillNo { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://bspgw.sf-express.com/std/service";
            }
            return "https://sfapi-sbox.sf-express.com/std/service";
        }

        public override string GetServiceCode()
        {
            return "EXP_RECE_VALIDATE_WAYBILLNO";
        }
    }
}
