using Com.Scm.Dvo;

namespace Com.Scm.Yms.Fac.Build.Dvo
{
    /// <summary>
    /// 楼宇
    /// </summary>
    public class YmsFacBuildDvo : ScmDataDvo
    {
        /// <summary>
        /// 所属园区
        /// </summary>
        public long area_id { get; set; }
        public string area_names { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 楼宇编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string namec { get; set; }
    }
}