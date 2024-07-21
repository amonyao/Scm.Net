using Com.Scm.Express.My.Shipper.SF.Scs.Model;
using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.SF.Scs.OrderSearch
{
    public class SfScsOrderSearchResponse
    {
        public Order order { get; set; }

        public List<OrderService> orderServiceList { get; set; }

        public List<OrderGoods> orderGoodsList { get; set; }

        public OrderReturn orderReturn { get; set; }
    }
}
