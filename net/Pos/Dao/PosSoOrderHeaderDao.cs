using Com.Scm.Dao.Unit;
using Com.Scm.Pos.Enums;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 订单头档
    /// </summary>
    [SugarTable("pos_so_order_header")]
    public class PosSoOrderHeaderDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int item_need_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int item_real_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int unit_need_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int unit_real_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SoHandleEnums handle { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            codes = UidUtils.NextCodes("pos_so_order_header");
            //codec = UidUtils.NextCodes("");
        }
    }
}