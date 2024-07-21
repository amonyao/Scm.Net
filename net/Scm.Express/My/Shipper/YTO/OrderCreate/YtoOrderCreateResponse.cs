using Com.Scm.Express.OrderCreate;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YTO.OrderCreate
{
    public class YtoOrderCreateResponse : YtoResponse
    {
        public YtoOrderCreateResult data { get; set; }

        public void ToResponse(OrderCreateResponse response)
        {
            var result = new OrderCreateResult();
            result.code = data.logisticsNo;
            result.mail_no = data.mailNo;
            result.opt_code = data.shortAddress;
            response.Append(result);
        }
    }

    public class YtoOrderCreateResult
    {
        /// <summary>
        /// 客户编码（K开头）
        /// </summary>
        public string customerCode { get; set; }

        /// <summary>
        /// 物流单号，打印拉取运单号前，物流单号和渠道唯一确定一笔快递物流订单。
        /// </summary>
        public string logisticsNo { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string mailNo { get; set; }

        /// <summary>
        /// 三段码
        /// </summary>
        public string shortAddress { get; set; }

        /// <summary>
        /// 面单打印，脱敏字段及对应值列表
        /// </summary>
        public List<YtoSecretWaybillRo> secretWaybills { get; set; }
    }

    public class YtoSecretWaybillRo
    {
        /// <summary>
        /// 脱敏字段KEY，其值为下单请求接口中对应的字段名。
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 脱敏字段描述
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 脱敏字段KEY对应的VALUE值
        /// </summary>
        public string value { get; set; }
    }
}
