using Com.Scm.Dvo;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 商品
    /// </summary>
    public class PosResSpuDvo : ScmDataDvo
    {
        /// <summary>
        /// 品类ID
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}