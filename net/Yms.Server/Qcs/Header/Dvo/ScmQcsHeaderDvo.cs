using Com.Scm.Dvo;

namespace Com.Scm.Yms.Qcs.Header.Dvo
{
    /// <summary>
    /// 方案
    /// </summary>
    public class ScmQcsHeaderDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 方案编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 上级方案
        /// </summary>
        public long pid { get; set; }

        /// <summary>
        /// 队列数量
        /// </summary>
        public int qty { get; set; }
    }
}