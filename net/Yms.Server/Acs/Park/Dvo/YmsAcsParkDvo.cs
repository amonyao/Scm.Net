using Com.Scm.Dvo;

namespace Com.Scm.Yms.Acs.Park.Dvo
{
    /// <summary>
    /// 停车管理
    /// </summary>
    public class YmsAcsParkDvo : ScmDataDvo
    {
        /// <summary>
        /// 入园类型
        /// </summary>
        public AcsParkTypesEnums types { get; set; }

        /// <summary>
        /// 园区
        /// </summary>
        public long area_id { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        public long build_id { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public long floor_id { get; set; }

        /// <summary>
        /// 房间
        /// </summary>
        public long room_id { get; set; }

        /// <summary>
        /// 所属商户
        /// </summary>
        public long merchant_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public long f_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public long t_time { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        public string driver { get; set; }
    }
}