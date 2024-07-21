using Com.Scm.Express.My;
using System.Collections.Generic;

namespace Com.Scm.Express.OrderSearch
{
    public class OrderSearchResponse : MyResponse<List<OrderSearchResult>>
    {
        public void Append(OrderSearchResult result)
        {
            if (result == null)
            {
                return;
            }

            if (data == null)
            {
                data = new List<OrderSearchResult>();
            }
            data.Add(result);
        }
    }

    public class OrderSearchResult
    {
        /// <summary>
        /// 自有单号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string mail_no { get; set; }
    }
}
