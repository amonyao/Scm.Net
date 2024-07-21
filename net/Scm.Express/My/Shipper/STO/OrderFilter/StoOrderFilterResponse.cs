namespace Com.Scm.Express.My.Shipper.STO.OrderFilter
{
    public class StoOrderFilterResponse : StoResponse<OrderFilterResult>
    {
    }

    public class OrderFilterResult
    {
        /// <summary>
        /// true为超区
        /// </summary>
        public bool outOfDelivery { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string comment { get; set; }
    }
}
