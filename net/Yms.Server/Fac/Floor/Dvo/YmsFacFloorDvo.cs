using Com.Scm.Dvo;

namespace Com.Scm.Yms.Fac
{
    /// <summary>
    /// 楼层
    /// </summary>
    public class YmsFacFloorDvo : ScmDataDvo
    {
        /// <summary>
        /// 所属园区
        /// </summary>
        public long area_id { get; set; }

        /// <summary>
        /// 所属楼宇
        /// </summary>
        public long build_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 楼层编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string namec { get; set; }
    }
}