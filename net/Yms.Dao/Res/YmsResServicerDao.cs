using Com.Scm.Dao.Unit;
using Com.Scm.Uid;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Res
{
    /// <summary>
    /// 服务商
    /// </summary>
    [SugarTable("yms_res_servicer")]
    public class YmsResServicerDao : ScmUnitDataDao
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 服务商编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 服务商名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.codes = UidHelper.NextCodes("yms_res_servicer");
            this.names = this.namec.Trim();
        }
    }
}