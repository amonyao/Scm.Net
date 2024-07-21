using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.JT.OrderFilter
{
    public class JtOrderFilterRequest : JtRequest
    {
        /// <summary>
        /// 寄件省份
        /// </summary>
        public string senderProvName { get; set; }
        /// <summary>
        /// 寄件城市
        /// </summary>
        public string senderCityName { get; set; }
        /// <summary>
        /// 寄件区域
        /// </summary>
        public string senderAreaName { get; set; }
        /// <summary>
        /// 寄件详细地址
        /// </summary>
        public string senderAddress { get; set; }

        /// <summary>
        /// 派件省份
        /// </summary>
        public string receiverProvName { get; set; }
        /// <summary>
        /// 派件城市
        /// </summary>
        public string receiverCityName { get; set; }
        /// <summary>
        /// 派件区域
        /// </summary>
        public string receiverAreaName { get; set; }
        /// <summary>
        /// 派件详细地址
        /// </summary>
        public string receiverAddress { get; set; }

        public override string GetPath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.jtexpress.com.cn/webopenplatformapi/api/route/rangeCheck";
            }
            return "https://uat-openapi.jtexpress.com.cn/webopenplatformapi/api/route/rangeCheck?uuid=9c87b5e9c41d441ab06f31db51d79c15";
        }
    }
}
