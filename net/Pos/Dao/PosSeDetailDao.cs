using Com.Scm.Dao;
using SqlSugar;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 促销明细
    /// </summary>
    [SugarTable("pos_se_detail")]
    public class PosSeDetailDao : ScmDataDao
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public long se_id { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 规格ID
        /// </summary>
        public long spec_id { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; }
    }
}