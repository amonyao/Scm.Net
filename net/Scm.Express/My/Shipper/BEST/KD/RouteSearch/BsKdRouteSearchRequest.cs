using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.BEST.KD.RouteSearch
{
    public class BsKdRouteSearchRequest : BsKdRequest
    {
        public MailNos mailNos { get; set; }

        public override string GetServiceType()
        {
            return "KD_TRACE_QUERY";
        }
    }

    public class MailNos
    {
        public List<string> mailNo { get; set; }
    }
}
