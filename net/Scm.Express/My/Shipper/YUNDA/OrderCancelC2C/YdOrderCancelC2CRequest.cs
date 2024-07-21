using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCancelC2C
{
    /// <summary>
    /// 散单取消
    /// </summary>
    public class YdOrderCancelC2CRequest : YdRequest
    {
        /// <summary>
        /// app_key
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 原样返回字段
        /// </summary>
        public string backparam { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.yundaex.com/openapi-api/v1/order/cancelOrder";
            }
            return "https://u-openapi.yundasys.com/openapi-api/v1/order/cancelOrder";
        }
    }
}
