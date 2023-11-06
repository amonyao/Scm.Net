using Com.Scm.Dao.Unit;
using Com.Scm.Uid;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Dev
{
    /// <summary>
    /// 考勤
    /// </summary>
    [SugarTable("yms_dev_attendance")]
    public class YmsDevAttendanceDao : ScmUnitDataDao
    {
        /// <summary>
        /// 所属园区
        /// </summary>
        [Required]
        public long area_id { get; set; }

        /// <summary>
        /// 所属楼宇
        /// </summary>
        [Required]
        public long build_id { get; set; }

        /// <summary>
        /// 所属楼层
        /// </summary>
        [Required]
        public long floor_id { get; set; }

        /// <summary>
        /// 所属房间
        /// </summary>
        [Required]
        public long room_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 考勤编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 考勤名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.codes = UidHelper.NextCodes("yms_dev_attendance");
            this.names = this.namec.Trim();
        }
    }
}