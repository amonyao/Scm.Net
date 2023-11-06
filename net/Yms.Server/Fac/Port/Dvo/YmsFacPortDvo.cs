using Com.Scm.Dvo;

namespace Com.Scm.Yms.Fac
{
    /// <summary>
    /// 大门管理
    /// </summary>
    public class YmsFacPortDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long area_id { get; set; }

        /// <summary>
        /// 大门类型（出口、入口、出入口）
        /// </summary>
        public FacPortModesEnums types { get; set; }

        /// <summary>
        /// 出入模式（人行、车行）
        /// </summary>
        public FacPortModesEnums modes { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 大门编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 大门名称
        /// </summary>
        public string namec { get; set; }
    }
}