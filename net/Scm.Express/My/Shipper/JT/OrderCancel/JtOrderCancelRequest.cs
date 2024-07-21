using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.JT.OrderCancel
{
    public class JtOrderCancelRequest : JtRequest
    {
        /// <summary>
        /// 客户编码（订单类型传2时，必填）
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 签名，Base64(Md5(客户编号+密文+privateKey))，其中密文：MD5(明文密码+jadada236t2) 后大写
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 订单类型 1（散客），2（协议客户）
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 客户订单号,传客户自己系统的订单号
        /// </summary>
        public string txlogisticId { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string reason { get; set; }

        public override string GetPath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://openapi.jtexpress.com.cn/webopenplatformapi/api/order/cancelOrder";
            }
            return "https://uat-openapi.jtexpress.com.cn/webopenplatformapi/api/order/cancelOrder?uuid=9c87b5e9c41d441ab06f31db51d79c15";
        }
    }
}
