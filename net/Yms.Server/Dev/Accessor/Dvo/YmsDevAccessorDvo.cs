using Com.Scm.Dvo;

namespace Com.Scm.Yms.Dev.Accessor.Dvo
{
    /// <summary>
    /// 门禁
    /// </summary>
    public class YmsDevAccessorDvo : ScmDataDvo
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
        /// 所属楼层
        /// </summary>
        public long floor_id { get; set; }

        /// <summary>
        /// 所属房间
        /// </summary>
        public long room_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 门禁编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 门禁名称
        /// </summary>
        public string namec { get; set; }
    }
}