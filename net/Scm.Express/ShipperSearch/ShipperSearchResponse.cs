using Com.Scm.Express.My;
using System.Collections.Generic;

namespace Com.Scm.Express.ShipperSearch
{
    public class ShipperSearchResponse : MyResponse<List<ShipperSearchResult>>
    {
        public void Append(ShipperSearchResult result)
        {
            if (result == null)
            {
                return;
            }

            if (data == null)
            {
                data = new List<ShipperSearchResult>();
            }
            data.Add(result);
        }
    }

    public class ShipperSearchResult
    {
        public string code { get; set; }
        public string name { get; set; }
    }
}
