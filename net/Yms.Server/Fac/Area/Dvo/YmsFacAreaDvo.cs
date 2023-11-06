using Com.Scm.Dvo;

namespace Com.Scm.Yms.Fac.Area.Dvo
{
    /// <summary>
    /// 园区管理
    /// </summary>
    public class YmsFacAreaDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 园区编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 园区名称
        /// </summary>
        public string namec { get; set; }
    }
}