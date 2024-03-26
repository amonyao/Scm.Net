using Com.Scm.Dao.Unit;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.So
{
    /// <summary>
    /// 订单主档
    /// </summary>
    [SugarTable("oms_so_header")]
    public class OmsSoHeaderDao : ScmUnitDataDao
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        [StringLength(32)]
        public string codes { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(64)]
        public string codec { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        [Required]
        public int sale_amt { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        [Required]
        public int paid_amt { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            codes = UidUtils.NextCodes("oms_so_header");
        }
    }
}