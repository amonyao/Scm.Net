using Com.Scm.Express.Dto;
using Com.Scm.Express.My;

namespace Com.Scm.Express.OrderFilter
{
    public class OrderFilterRequest : MyRequest
    {
        public OrderHeader Order { get; set; }
    }
}
