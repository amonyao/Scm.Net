namespace Com.Scm.Express.My.Shipper.YTO.OrderCancel
{
    public class YtoOrderCancelResponse : YtoResponse
    {
        public OrderCancelResult data { get; set; }
    }

    public class OrderCancelResult
    {
        /// <summary>
        /// 客户K码
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 物流单号，打印拉取运单号前，物流单号和渠道唯一确定一笔快递物流订单。
        /// </summary>
        public string logisticsNo { get; set; }
    }
}
