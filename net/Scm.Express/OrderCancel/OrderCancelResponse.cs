using Com.Scm.Express.My;
using System.Collections.Generic;

namespace Com.Scm.Express.OrderCancel
{
    public class OrderCancelResponse : MyResponse<List<OrderCancelResult>>
    {
    }

    public class OrderCancelResult
    {
        public string code { get; set; }
        public string bill_no { get; set; }
        public string mail_no { get; set; }
    }
}
