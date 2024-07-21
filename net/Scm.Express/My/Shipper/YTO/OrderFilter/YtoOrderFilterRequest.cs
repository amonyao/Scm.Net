using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.YTO.OrderFilter
{
    public class YtoOrderFilterRequest : YtoRequest
    {
        /// <summary>
        /// 始发国家,国内价格默认‘CN’
        /// </summary>
        public string fromCountry { get; set; }
        /// <summary>
        /// 目的国家,国内价格默认‘CN’
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 始发省份
        /// </summary>
        public string startProv { get; set; }
        /// <summary>
        /// 始发城市
        /// </summary>
        public string startCity { get; set; }
        /// <summary>
        /// 始发区、县
        /// </summary>
        public string startCountry { get; set; }
        /// <summary>
        /// 始发街道
        /// </summary>
        public string startTown { get; set; }
        /// <summary>
        /// 始发详细地址
        /// </summary>
        public string startAddress { get; set; }
        /// <summary>
        /// 目的省份
        /// </summary>
        public string endProv { get; set; }
        /// <summary>
        /// 目的城市
        /// </summary>
        public string endCity { get; set; }
        /// <summary>
        /// 目的区、县
        /// </summary>
        public string endCountry { get; set; }
        /// <summary>
        /// 目的街道
        /// </summary>
        public string endTown { get; set; }
        /// <summary>
        /// 目的详细地址
        /// </summary>
        public string endAddress { get; set; }
        /// <summary>
        /// 快件重量,重量和长宽高二者选其一必填
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 快件长度,重量和长宽高二者选其一必填	
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 快件宽度,重量和长宽高二者选其一必填
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 快件高度,重量和长宽高二者选其一必填	
        /// </summary>
        public string height { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "https://openuat.yto56.com.cn:6443/open/inreach_adapter/v1/PFUYji/TEST";
        }

        public override string GetServiceCode()
        {
            return "korder_cancel_adapter";
        }

        public override string GetServiceVer()
        {
            return "v1";
        }

        public override string GetPartnerCode()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "TEST";
        }

        public override string GetPartnerPass()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "";
            }
            return "123456";
        }
    }
}
