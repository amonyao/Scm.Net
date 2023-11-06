using Com.Scm.Dvo;

namespace Com.Scm.Yms.Acs.Apply.Dvo
{
    /// <summary>
    /// 入园申请
    /// </summary>
    public class YmsAcsApplyDvo : ScmDataDvo
    {
        /// <summary>
        /// 申请类型
        /// </summary>
        public AcsApplyTypesEnums types { get; set; }

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
        /// 申请原因
        /// </summary>
        public long reason { get; set; }

        /// <summary>
        /// 入园人数
        /// </summary>
        public int person { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        public string driver { get; set; }
    }
}