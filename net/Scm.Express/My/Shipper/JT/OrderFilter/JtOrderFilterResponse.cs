using Com.Scm.Express.OrderFilter;

namespace Com.Scm.Express.My.Shipper.JT.OrderFilter
{
    public class JtOrderFilterResponse : JtResponse<JtOrderFilterResult>
    {
        public void ToResponse(OrderFilterResponse response)
        {
            var result = new OrderFilterResult();
            result.usable = data.rangeCheckResult == 1 ? UsableEnum.Success : UsableEnum.Failure;
            result.reason = data.abnormalReason;
            response.data = result;
        }
    }

    public class JtOrderFilterResult
    {
        /// <summary>
        /// 是否可达： 1.可达 2.不可达
        /// </summary>
        public int rangeCheckResult { get; set; }
        /// <summary>
        /// 异常原因
        /// </summary>
        public string abnormalReason { get; set; }
        /// <summary>
        /// 异常类型
        /// </summary>
        public string abnormalType { get; set; }
    }
}
