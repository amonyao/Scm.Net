using Com.Scm.Dao.Unit;
using Com.Scm.Pos.Enums;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 运费
    /// </summary>
    [SugarTable("pos_res_freight")]
    public class PosResFreightDao : ScmUnitDataDao
    {
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 运费编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 运费名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 计费方案
        /// </summary>
        [Required]
        public FreightCostTypesEnums cost_types { get; set; }

        /// <summary>
        /// 包邮方案
        /// </summary>
        [Required]
        public FreightFreeTypesEnums free_types { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            //if (string.IsNullOrWhiteSpace(names))
            //{
            //    names = namef;
            //}
            codes = UidUtils.NextCodes("pos_res_freight");
        }
    }
}