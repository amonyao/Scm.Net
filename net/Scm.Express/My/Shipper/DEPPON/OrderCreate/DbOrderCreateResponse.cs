using Com.Scm.Express.OrderCreate;

namespace Com.Scm.Express.My.Shipper.DEPPON.OrderCreate
{
    public class DbOrderCreateResponse : DbResponse
    {
        /// <summary>
        /// 渠道单号
        /// 失败返回订单号（对方下单的订单号）
        /// </summary>
        public string logisticID { get; set; }
        /// <summary>
        /// 运单号集合（多个运单号）
        /// 比如：DPK360000000000,DPK380000000001,DPK380000000002,生产环境一般DPK36开头为母单号且排在第一位，零担一个订单无论多少件仅会返回一个运单号
        /// </summary>
        public string mailNo { get; set; }
        /// <summary>
        /// 目的站部门简称
        /// 零担电子面单返回到达部门简称，快递电子面单返回大头笔加二段码
        /// </summary>
        public string arrivedOrgSimpleName { get; set; }
        /// <summary>
        /// 最终到达外场
        /// 零担电子面单标签信息
        /// </summary>
        public string arrivedOutFieldName { get; set; }
        /// <summary>
        /// 提货网点编码
        /// 用于零担面单条码生成使用,德邦零担面单条码生成规则:运单号+4位流水号+到达部门简码(该字段值)
        /// </summary>
        public string stationNo { get; set; }
        /// <summary>
        /// 超远派送金额
        /// 超派加收的费用，实际以营业部说法为准
        /// </summary>
        public string muchHigherDelivery { get; set; }
        /// <summary>
        /// 母单号标识
        /// 仅快递电子面单且为子母件场景下才有值
        /// </summary>
        public string parentMailNo { get; set; }

        public void ToResponse(OrderCreateResponse response)
        {
            var result = new OrderCreateResult();
            result.code = logisticID;
            result.mail_no = mailNo;
            response.Append(result);
        }
    }
}
