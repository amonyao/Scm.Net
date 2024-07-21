namespace Com.Scm.Express.My.Shipper.DEPPON.RouteSearch
{
    public class DbRouteSearchRequest : DbRequest
    {
        /// <summary>
        /// 德邦运单号
        /// </summary>
        public string mailNo { get; set; }

        public override string GetServicePath()
        {
            if (MyExpress.Environment == Dto.EnvironmentEnum.Release)
            {
                return "http://dpapi.deppon.com/dop-interface-sync/standard-query/newTraceQuery.action";
            }

            return "http://dpsanbox.deppon.com/sandbox-web/standard-order/newTraceQuery.action";
        }

        public override string GetServiceCode()
        {
            return "NEW_TRACE_QUERY";
        }
    }
}
