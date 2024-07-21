using System.ComponentModel;

namespace Com.Scm.Express.My.Shipper.SF.Exp.OrderFilter
{
    public class SfOrderFilterResponse
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 筛单结果：
        /// 1：人工确认
        /// 2：可收派
        /// 3：不可以收派
        /// 4 : 地址无法确认
        /// 当filter_type = 1时，不存在1值
        /// </summary>
        public FilterResult filterResult { get; set; }
        /// <summary>
        /// 原寄地区域代码，如果可收派，此项不能为空
        /// </summary>
        public string originCode { get; set; }
        /// <summary>
        /// 目的地区域代码，如果可收派，此项不能为空
        /// </summary>
        public string destCode { get; set; }
        /// <summary>
        /// 如果filter_result=3时为必填，不可以收派的原因代码：
        /// 1：收方超范围
        /// 2：派方超范围
        /// 3：其它原因
        /// </summary>
        public string remark { get; set; }
    }

    public enum FilterResult
    {
        [Description("人工确认")]
        Result1,
        [Description("可收派")]
        Result2,
        [Description("不可以收派")]
        Result3,
        [Description("地址无法确认")]
        Result4
    }
}
