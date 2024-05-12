using Com.Scm.Dvo;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 规格信息
    /// </summary>
    public class PosResSpuSpecDvo : ScmDataDvo
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 规格编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public int price { get; set; }
    }
}