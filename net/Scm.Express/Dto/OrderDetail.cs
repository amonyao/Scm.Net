namespace Com.Scm.Express.Dto
{
    public class OrderDetail
    {
        /// <summary>
        /// 货品编码
        /// </summary>
        public string goods_code { get; set; }
        /// <summary>
        /// 货品名称
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 货品数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 长（厘米）
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// 宽（厘米）
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 高（厘米）
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 体积（立方厘米）
        /// </summary>
        public long volume { get; set; }

        /// <summary>
        /// 重量（克）
        /// </summary>
        public int weight { get; set; }
    }
}
