using Com.Scm.Dvo;

namespace Com.Scm.Fes.Doc
{
    /// <summary>
    /// 图像软链
    /// </summary>
    public class FileCatDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 图像
        /// </summary>
        public long file_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public char salt { get; set; }
    }
}