using Com.Scm.Dvo;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 订单明细
    /// </summary>
    public class PosSoOrderDetailDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long order_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 规格ID
        /// </summary>
        public long spec_id { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public string exts { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public int need_qty { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        public int real_qty { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public int total { get; set; }
    }
}