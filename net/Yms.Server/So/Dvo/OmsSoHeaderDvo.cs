using Com.Scm.Dvo;

namespace Com.Scm.So
{
    /// <summary>
    /// 订单主档
    /// </summary>
    public class OmsSoHeaderDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int sale_amt { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        public int paid_amt { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string remark { get; set; }
    }
}