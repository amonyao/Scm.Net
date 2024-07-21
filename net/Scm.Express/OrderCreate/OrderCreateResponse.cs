using Com.Scm.Express.My;
using System.Collections.Generic;

namespace Com.Scm.Express.OrderCreate
{
    public class OrderCreateResponse : MyResponse<List<OrderCreateResult>>
    {
        public void Append(OrderCreateResult result)
        {
            if (result == null)
            {
                return;
            }

            if (data == null)
            {
                data = new List<OrderCreateResult>();
            }
            data.Add(result);
        }
    }

    public class OrderCreateResult
    {
        /// <summary>
        /// 自有单号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 三方单号
        /// </summary>
        public string bill_no { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string mail_no { get; set; }
        public string opt_code { get; set; }
        public string safe_no { get; set; }
    }
}