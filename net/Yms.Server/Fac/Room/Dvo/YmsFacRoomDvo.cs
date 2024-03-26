using Com.Scm.Dvo;

namespace Com.Scm.Yms.Fac.Room.Dvo
{
    /// <summary>
    /// 房间
    /// </summary>
    public class YmsFacRoomDvo : ScmDataDvo
    {
        /// <summary>
        /// 所属园区
        /// </summary>
        public long area_id { get; set; }
        public string area_names { get; set; }

        /// <summary>
        /// 所属楼宇
        /// </summary>
        public long build_id { get; set; }
        public string build_names { get; set; }

        /// <summary>
        /// 所属楼层
        /// </summary>
        public long floor_id { get; set; }
        public string floor_names { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 房间编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        public string namec { get; set; }
    }
}