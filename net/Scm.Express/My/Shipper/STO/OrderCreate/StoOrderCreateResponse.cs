using Com.Scm.Express.OrderCreate;

namespace Com.Scm.Express.My.Shipper.STO.OrderCreate
{
    public class StoOrderCreateResponse : StoResponse<StoOrderCreateResult>
    {
        public void ToResponse(OrderCreateResponse response)
        {
            var result = new OrderCreateResult();
            result.code = data.orderNo;
            result.mail_no = data.waybillNo;
            result.safe_no = data.safeNo;
            result.opt_code = data.bigWord;
            response.Append(result);
        }
    }

    public class StoOrderCreateResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybillNo { get; set; }
        /// <summary>
        /// 大字/三段码
        /// </summary>
        public string bigWord { get; set; }
        /// <summary>
        /// 集包地
        /// </summary>
        public string packagePlace { get; set; }
        /// <summary>
        /// 客户订单号（调度订单时返回客户订单号，非调度订单不返回该值）
        /// </summary>
        public string sourceOrderId { get; set; }
        /// <summary>
        /// 安全号码
        /// </summary>
        public string safeNo { get; set; }
    }
}
