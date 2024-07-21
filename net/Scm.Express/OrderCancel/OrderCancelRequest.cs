using System.ComponentModel;
using Com.Scm.Express.Dto;
using Com.Scm.Express.My;

namespace Com.Scm.Express.OrderCancel
{
    public class OrderCancelRequest : MyRequest
    {
        public OrderHeader Order { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
        public CancelType CancelType { get; set; }
        public int CancelCode { get; set; }
    }

    public enum CancelType
    {
        None = 0,
        [Description("用户发起")]
        ByCustomer,
        [Description("超时因素")]
        ByDelay
    }
}
