using System.Collections.Generic;
using System.Diagnostics;

namespace Com.Scm.Express.My.Shipper.BEST.KD.RouteSearch
{
    public class BsKdRouteSearchResponse : BsKdResponse
    {
        public List<TraceLogs> traceLogs { get; set; }
    }

    public class TraceLogs
    {
        public bool check { get; set; }
        public Problems problems { get; set; }
        public string mailNo { get; set; }
        public Traces traces { get; set; }
    }

    public class Traces
    {
        public List<Trace> trace { get; set; }
    }

    public class Problems
    {
        public List<Problem> problem { get; set; }
    }

    public class Problem
    {
        public long? seqNum { get; set; }
        public string problemType { get; set; }
        public string registerMan { get; set; }
        public string registerDate { get; set; }
        public string registerSite { get; set; }
        public string problemCause { get; set; }
        public string noticeSite { get; set; }
        public string replyMan { get; set; }
        public string replyDate { get; set; }
        public string replyContent { get; set; }
    }
}
