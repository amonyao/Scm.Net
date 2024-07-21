using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.YUNDA.Model;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCreateB2C
{
    /// <summary>
    /// 电子面单下单
    /// </summary>
    public class YdOrderCreateB2CRequest : YdRequest
    {
        /// <summary>
        /// 合作商appid（等同app-key）
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 韵达白马账号（合作网点提供）
        /// </summary>
        public string partner_id { get; set; }
        /// <summary>
        /// 韵达白马账号的联调密码（合作网点提供）
        /// </summary>
        public string secret { get; set; }
        /// <summary>
        /// 订单详情
        /// </summary>
        public List<OrderCreateInfo> orders { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.yundaex.com/openapi-api/v1/accountOrder/createBmOrder";
            }
            return "https://u-openapi.yundasys.com/openapi-api/v1/accountOrder/createBmOrder";
        }
    }

    public class MultiPackInfo
    {
        /// <summary>
        /// 是否一票多件一票多件必填 是：填1； 否：空，不填 (必须走增值服务才能生效{"type":"MUL","markingValue":{"value":10(总单量)}})
        /// </summary>
        public string mulpck { get; set; }
        /// <summary>
        /// 总包裹数量：当最后一件时，传输此值
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 结束标记,当最后一件时,传输此值 0:否，1:是
        /// </summary>
        public int endmark { get; set; }
    }
}
