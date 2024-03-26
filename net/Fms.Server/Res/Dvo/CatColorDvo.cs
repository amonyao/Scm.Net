using Com.Scm.Dvo;

namespace Com.Scm.Fms.Res.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class CatColorDvo : ScmDataDvo
    {
        /// <summary>
        /// 类别
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public long color_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int qty { get; set; }
    }
}