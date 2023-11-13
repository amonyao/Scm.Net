using Com.Scm.Dvo;

namespace Com.Scm.Yms.Qcs.Queue.Dvo
{
    /// <summary>
    /// 号码
    /// </summary>
    public class ScmQcsQueueDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long detail_id { get; set; }

        /// <summary>
        /// 排队序号
        /// </summary>
        public int idx { get; set; }

        /// <summary>
        /// 排队号码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string contact { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 呼叫次数
        /// </summary>
        public int calling { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public int handle { get; set; }
    }
}