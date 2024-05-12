using Com.Scm.Dvo;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 促销明细
    /// </summary>
    public class PosSeDetailDvo : ScmDataDvo
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