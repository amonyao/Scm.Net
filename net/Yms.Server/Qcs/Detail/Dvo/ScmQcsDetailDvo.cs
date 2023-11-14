using Com.Scm.Dvo;

namespace Com.Scm.Yms.Qcs.Detail.Dvo
{
    /// <summary>
    /// 队列
    /// </summary>
    public class ScmQcsDetailDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 队列编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 队列说明
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 当前号码
        /// </summary>
        public int idx { get; set; }

        /// <summary>
        /// 号码前缀
        /// </summary>
        public string prefix { get; set; }

        /// <summary>
        /// 数字长度
        /// </summary>
        public int length { get; set; }
    }
}