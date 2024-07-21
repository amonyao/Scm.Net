using Com.Scm.Express.Dto;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCancel
{
    public class YdOrderCancelRequest : YdRequest
    {
        /// <summary>
        /// app_key
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 韵达大客户账号（韵达业务方提供）
        /// </summary>
        public string partner_id { get; set; }
        /// <summary>
        /// 韵达大客户秘钥（韵达业务方提供）
        /// </summary>
        public string secret { get; set; }
        /// <summary>
        /// 订单详情
        /// </summary>
        public List<OrderCancelInfo> orders { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.yundaex.com/openapi-api/v1/accountOrder/cancelBmOrder";
            }
            return "https://u-openapi.yundasys.com/openapi-api/v1/accountOrder/cancelBmOrder";
        }
    }

    public class OrderCancelInfo
    {
        /// <summary>
        /// 客户订单号 由字母、数字、下划线组成，必须保证唯一，请对特殊符号进行过滤
        /// </summary>
        public string order_serial_no { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string mailno { get; set; }
    }
}
