using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Dvo
{
    /// <summary>
    /// 图片颜色
    /// </summary>
    public class ImageColorDvo : ScmDataDvo
    {
        /// <summary>
        /// 图像
        /// </summary>
        public long file_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; } = 0;

        /// <summary>
        /// 颜色
        /// </summary>
        public long color_id { get; set; }
    }
}