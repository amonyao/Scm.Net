using Com.Scm.Dto;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 规格销售信息（可计算）
    /// </summary>
    public class PosSkuSaleDto : ScmDataDto
    {
        /// <summary>
        /// 市场价格
        /// </summary>
        public int market_price { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public int tax_rate { get; set; }

        /// <summary>
        /// 销售积分
        /// </summary>
        public int point { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        public int vip_point { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int stock { get; set; }
    }
}