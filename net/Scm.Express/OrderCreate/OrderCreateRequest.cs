using Com.Scm.Express.Dto;
using Com.Scm.Express.My;
using System.Collections.Generic;

namespace Com.Scm.Express.OrderCreate
{
    public class OrderCreateRequest : MyRequest
    {
        public List<OrderHeader> Orders { get; set; }

        public BusinessTypeEnum Business { get; set; }

        public void Append(OrderHeader order)
        {
            if (order == null)
            {
                return;
            }

            if (Orders == null)
            {
                Orders = new List<OrderHeader>();
            }
            Orders.Add(order);
        }

        public bool HasItems()
        {
            return Orders != null && Orders.Count > 0;
        }
    }
}
