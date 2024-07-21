using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderUpdate
{
    public class YdOrderUpdateResponse : YdResponse<List<OrderUpdateResult>>
    {
    }

    public class OrderUpdateResult
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string order_serial_no { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string mailno { get; set; }
        /// <summary>
        /// pdf参数信息（根据客户的需求返回json格式数据或对json格式进行加密后的数据）
        /// </summary>
        public string pdf_info { get; set; }
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
