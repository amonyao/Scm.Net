namespace Com.Scm.Express.My.Shipper.DEPPON.OrderSearch
{
    public class DbOrderSearchRequest : DbRequest
    {
        /// <summary>
        /// 物流公司ID
        /// </summary>
        public string logisticCompanyID { get; set; }

        /// <summary>
        /// 渠道单号
        /// </summary>
        public string logisticID { get; set; }

        public override string GetServicePath()
        {
            if (DbExpress.Environment == Dto.EnvironmentEnum.Release)
            {
                return "http://dpapi.deppon.com/dop-interface-sync/standard-query/queryOrder.action";
            }

            return "http://dpsanbox.deppon.com/sandbox-web/standard-order/queryOrder.action";
        }

        public override string GetServiceCode()
        {
            return "QUERY_LOGISTICS_ORDER";
        }
    }
}
