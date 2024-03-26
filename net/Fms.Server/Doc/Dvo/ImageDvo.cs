namespace Com.Scm.Fms.Doc.Dvo
{
    /// <summary>
    /// 图片数据
    /// </summary>
    public class ImageDvo : FileDvo
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// EXIF信息
        /// </summary>
        public string exif { get; set; }
    }
}