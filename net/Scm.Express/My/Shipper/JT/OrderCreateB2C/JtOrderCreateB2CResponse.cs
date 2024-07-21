using Com.Scm.Express.OrderCreate;

namespace Com.Scm.Express.My.Shipper.JT.OrderCreateB2C
{
    public class JtOrderCreateB2CResponse : JtResponse<JtOrderCreateResultB2C>
    {
        public void ToResponse(OrderCreateResponse response)
        {
            var result = new OrderCreateResult();
            result.code = data.txlogisticId;
            result.mail_no = data.billCode;
            response.Append(result);
        }
    }

    public class JtOrderCreateResultB2C
    {
        /// <summary>
        /// 集包地
        /// <summary>
        public string lastCenterName { get; set; }
        /// <summary>
        /// 返回客户订单号
        /// <summary>
        public string txlogisticId { get; set; }
        /// <summary>
        /// 订单创建时间 yyyy-MM-dd HH:mm:ss
        /// <summary>
        public string createOrderTime { get; set; }
        /// <summary>
        /// 运单号
        /// <summary>
        public string billCode { get; set; }
        /// <summary>
        /// 三段码（获取到三段码优先返回三段码，无三段码返回大头笔）
        /// </summary>
        public string sortingCode { get; set; }
        /// <summary>
        /// 参考总运费（数值型）
        /// </summary>
        public string sumFreight { get; set; }
    }
}
