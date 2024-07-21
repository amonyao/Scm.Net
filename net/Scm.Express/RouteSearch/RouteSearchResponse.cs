using Com.Scm.Express.My;
using System.Collections.Generic;

namespace Com.Scm.Express.RouteSearch
{
    public class RouteSearchResponse : MyResponse<Dictionary<string, List<RouteSearchResult>>>
    {
        public void Append(string key, List<RouteSearchResult> results)
        {
            if (results == null)
            {
                return;
            }
            if (data == null)
            {
                data = new Dictionary<string, List<RouteSearchResult>>();
            }

            data[key] = results;
        }
    }

    public class RouteSearchResult
    {
        public string mail_no { get; set; }

        public string opt_time { get; set; }
        public string opt_code { get; set; }

        public string opt_user_code { get; set; }
        public string opt_user_name { get; set; }
        public string opt_user_phone { get; set; }

        public string opt_unit_code { get; set; }
        public string opt_unit_name { get; set; }
        public string opt_unit_phone { get; set; }

        public string remark { get; set; }
    }
}
