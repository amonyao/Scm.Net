namespace Com.Scm.Express.My.Shipper.BEST.KY.OrderFilter
{
    public class BsKyOrderFilterResponse : BsKyResponse
    {
        public bool isArrived { get; set; }
        public ReachableSiteVo reachableSiteVo { get; set; }
    }

    public class ReachableSiteVo
    {
        public string siteCode { get; set; }
        public string siteName { get; set; }
        public string location { get; set; }
        public string principal { get; set; }
        public string phone { get; set; }
        public string salePhone { get; set; }
        public string dispatchRange { get; set; }
        public bool isSelfPeck { get; set; }
    }
}
