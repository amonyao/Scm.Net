using Com.Scm.Dvo;

namespace Com.Scm.Yms.Res.Servicer.Dvo
{
    /// <summary>
    /// 服务商
    /// </summary>
    public class YmsResServicerDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 服务商编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 服务商名称
        /// </summary>
        public string namec { get; set; }
    }
}