using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderFilter
{
    public class YdOrderFilterRequest : YdRequest
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 发件地址
        /// </summary>
        public string sender_address { get; set; }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string receiver_address { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.yundaex.com/openapi-api/v1/bm/queryRoute";
            }
            return "https://u-openapi.yundasys.com/openapi-api/v1/bm/queryRoute";
        }
    }
}
