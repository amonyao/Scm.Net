using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.YUNDA.Model;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderUpdate
{
    public class YdOrderUpdateRequest : YdRequest
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
                return "https://openapi.yundaex.com/openapi-api/v1/accountOrder/updateBmOrder";
            }
            return "https://u-openapi.yundasys.com/openapi-api/v1/accountOrder/updateBmOrder";
        }
    }
}
