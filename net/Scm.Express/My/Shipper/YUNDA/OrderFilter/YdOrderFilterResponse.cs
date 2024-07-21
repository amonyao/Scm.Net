using System.Collections.Generic;

namespace Com.Scm.Express.My.Shipper.YUNDA.OrderFilter
{
    public class YdOrderFilterResponse : YdResponse<OrderFilterResult>
    {
    }

    public class OrderFilterResult
    {
        public string msg { get; set; }
        public string status { get; set; }
        public List<OrderFilterItem> response { get; set; }
    }

    public class OrderFilterItem
    {
        /// <summary>
        /// 唯一值，和请求的id对应
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 关停状态 ：0或1，0：不关停，1：关停
        /// </summary>
        public string close { get; set; }
        /// <summary>
        /// 始发区划
        /// </summary>
        public string sender_region_code { get; set; }
        /// <summary>
        /// 始发揽件网点
        /// </summary>
        public string sender_branch_code { get; set; }
        /// <summary>
        /// 目的区划
        /// </summary>
        public string receiver_region_code { get; set; }
        /// <summary>
        /// 目的揽件网点
        /// </summary>
        public string receiver_branch_code { get; set; }
        /// <summary>
        /// 一段码
        /// </summary>
        public string one_code { get; set; }
        /// <summary>
        /// 二段码
        /// </summary>
        public string two_code { get; set; }
        /// <summary>
        /// 三段码
        /// </summary>
        public string three_code { get; set; }
        /// <summary>
        /// 集包地编码
        /// </summary>
        public string package_code { get; set; }
        /// <summary>
        /// 集包地名称
        /// </summary>
        public string package_name { get; set; }
        /// <summary>
        /// 关停一级原因只有关停状态close 为1 时 才会有关停原因
        /// </summary>
        public string close_reason_one { get; set; }
        /// <summary>
        /// 关停二级原因
        /// </summary>
        public string close_reason_two { get; set; }
        /// <summary>
        /// 是否预警关停网点：0或1，0：否，1：是
        /// </summary>
        public string warning_close_code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
