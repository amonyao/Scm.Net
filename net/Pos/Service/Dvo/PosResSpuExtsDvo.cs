using Com.Scm.Dvo;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public class PosResSpuExtsDvo : ScmDataDvo
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 属性内容
        /// </summary>
        public string json { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public int price { get; set; }
    }
}