using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Acs
{
    /// <summary>
    /// 黑白名单
    /// </summary>
    [SugarTable("yms_acs_special")]
    public class YmsAcsSpecialDao : ScmUnitDataDao
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public AcsSpecialTypesEnums types { get; set; }

        /// <summary>
        /// 实体（人员、车辆）
        /// </summary>
        [Required]
        public AcsSpecialModesEnums modes { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        [StringLength(128)]
        public string driver { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [StringLength(128)]
        public string reason { get; set; }
    }
}