using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderCancel
{
    public class YdOrderCancelResponse : YdResponse<List<OrderCancelResult>>
    {
    }

    public class OrderCancelResult
    {
        /// <summary>
        /// 订单唯一序列号
        /// </summary>
        public string order_serial_no { get; set; }
        /// <summary>
        /// 筛单成功，则不为空；筛单失败，则为空
        /// </summary>
        public string mailno { get; set; }
        /// <summary>
        /// 返回状态，1表示成功，0表示失败
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 成功或者错误的提示信息
        /// </summary>
        public string msg { get; set; }
    }
}
