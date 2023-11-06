using Com.Scm.Dao.Unit;
using Com.Scm.Uid;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Res
{
    /// <summary>
    /// 租客
    /// </summary>
    [SugarTable("yms_res_employee")]
    public class YmsResEmployeeDao : ScmUnitDataDao
    {
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
        /// 租客编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 租客名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.codes = UidHelper.NextCodes("yms_res_exployee");
            this.names = this.namec.Trim();
        }
    }
}