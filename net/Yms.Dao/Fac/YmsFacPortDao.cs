using Com.Scm.Dao.Unit;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Fac
{
    /// <summary>
    /// 大门管理
    /// </summary>
    [SugarTable("yms_fac_port")]
    public class YmsFacPortDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long area_id { get; set; }

        /// <summary>
        /// 大门类型（出口、入口、出入口）
        /// </summary>
        [Required]
        public FacPortTypesEnums types { get; set; }

        /// <summary>
        /// 出入模式（人行、车行）
        /// </summary>
        [Required]
        public FacPortModesEnums modes { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 大门编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 大门名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            this.codes = UidUtils.NextCodes("yms_fac_port");
            this.names = this.namec.Trim();
        }
    }
}