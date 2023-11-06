using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Acs
{
    /// <summary>
    /// 停车管理
    /// </summary>
    [SugarTable("yms_acs_park")]
    public class YmsAcsParkDao : ScmUnitDataDao
    {
        /// <summary>
        /// 入园类型
        /// </summary>
        [Required]
        public AcsParkTypesEnums types { get; set; }

        /// <summary>
        /// 园区
        /// </summary>
        [Required]
        public long area_id { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        [Required]
        public long build_id { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Required]
        public long floor_id { get; set; }

        /// <summary>
        /// 房间
        /// </summary>
        [Required]
        public long room_id { get; set; }

        /// <summary>
        /// 所属商户
        /// </summary>
        [Required]
        public long merchant_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
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
        [StringLength(128)]
        public string driver { get; set; }


    }
}