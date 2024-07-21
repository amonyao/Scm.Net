using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.YUNDA.Model;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCreateC2C
{
    /// <summary>
    /// 散件下单
    /// </summary>
    public class YdOrderCreateC2CRequest : YdRequest
    {
        /// <summary>
        /// 合作商app-key
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 合作商订单号
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 运单回传地址url
        /// </summary>
        public string backurl { get; set; }
        /// <summary>
        /// 寄件人信息
        /// </summary>
        public ContactInfo sender { get; set; }
        /// <summary>
        /// 收件人信息
        /// </summary>
        public ContactInfo receiver { get; set; }
        /// <summary>
        /// 取件开始时间(yyyy-MM-dd HH:mm:ss )
        /// </summary>
        public string sendstarttime { get; set; }
        /// <summary>
        /// 取件结束时间
        /// </summary>
        public string sendendtime { get; set; }
        /// <summary>
        /// 物品重量
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 货物大小（米），用半角的逗号来分隔长宽高
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 货物金额
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public string freight { get; set; }
        /// <summary>
        /// 保险费
        /// </summary>
        public string premium { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public string other_charges { get; set; }
        /// <summary>
        /// 商品信息集合
        /// </summary>
        public string items { get; set; }
        /// <summary>
        /// 原样返回字段
        /// </summary>
        public string backparam { get; set; }
        /// <summary>
        /// 商品类型(见商品类型字段)
        /// </summary>
        public string special { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.yundaex.com/openapi-api/v1/order/pushOrder";
            }
            return "https://u-openapi.yundasys.com/openapi-api/v1/order/pushOrder";
        }
    }
}
