using System.Collections.Generic;

namespace Com.Scm.Express.Dto
{
    public class OrderHeader
    {
        /// <summary>
        /// 单据号码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string mail_no { get; set; }

        /// <summary>
        /// 寄件人
        /// </summary>
        public Contact consigner { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public Contact consignee { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_time { get; set; }
        /// <summary>
        /// 下单备注
        /// </summary>
        public string remark { get; set; }

        #region 物品信息
        /// <summary>
        /// 整体长度（厘米）
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// 整体宽度（厘米）
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 整体高度（厘米）
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 整体体积（立方厘米）
        /// </summary>
        public long volume { get; set; }

        /// <summary>
        /// 整体重量（克）
        /// </summary>
        public int weight { get; set; }

        /// <summary>
        /// 物品类型
        /// </summary>
        public GoodsTypeEnum goods_type { get; set; }
        /// <summary>
        /// 物品说明
        /// </summary>
        public string goods_desc { get; set; }

        public int item_qty { get; set; }
        public int unit_qty { get; set; }

        /// <summary>
        /// 货物信息
        /// </summary>
        public List<OrderDetail> details { get; set; }
        #endregion

        #region 金额相关
        /// <summary>
        /// 货品总额（分）
        /// </summary>
        public int goods_amt { get; set; }
        /// <summary>
        /// 订单金额（分）
        /// </summary>
        public int order_amt { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public int insured_amt { get; set; }
        /// <summary>
        /// 已付金额（分）
        /// </summary>
        public int prepaid_amt { get; set; }
        /// <summary>
        /// 代收金额（分）
        /// </summary>
        public int payment_amt { get; set; }
        /// <summary>
        /// 代收方式（分）
        /// </summary>
        public int payment_type { get; set; }

        /// <summary>
        /// 运费价格（分）
        /// </summary>
        public int freight_amt { get; set; }
        /// <summary>
        /// 运费支付方式
        /// </summary>
        public int freight_type { get; set; }
        #endregion
    }
}
