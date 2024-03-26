using Com.Scm.Dao.Unit;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Fac
{
    /// <summary>
    /// 园区管理
    /// </summary>
    [SugarTable("yms_fac_area")]
    public class YmsFacAreaDao : ScmUnitDataDao
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 园区编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 园区名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.codes = UidUtils.NextCodes("yms_fac_area");
            this.names = this.namec.Trim();
        }
    }
}