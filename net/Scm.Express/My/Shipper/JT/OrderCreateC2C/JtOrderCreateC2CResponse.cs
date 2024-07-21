using Com.Scm.Express.OrderCreate;

namespace Com.Scm.Express.My.Shipper.JT.OrderCreateC2C
{
    public class JtOrderCreateC2CResponse : JtResponse<JtOrderCreateResultC2C>
    {
        public void ToResponse(OrderCreateResponse response)
        {
            var result = new OrderCreateResult();
            result.code = data.txlogisticId;
            result.mail_no = data.orderId;
            response.Append(result);
        }
    }

    public class JtOrderCreateResultC2C
    {
        /// <summary>
        /// 返回客户订单号
        /// <summary>
        public string txlogisticId { get; set; }
        /// <summary>
        /// 订单创建时间 yyyy-MM-dd HH:mm:ss
        /// <summary>
        public string createOrderTime { get; set; }
        /// <summary>
        /// 返回极兔订单号
        /// </summary>
        public string orderId { get; set; }
    }
}
