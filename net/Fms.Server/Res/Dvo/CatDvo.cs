using Com.Scm.Dvo;

namespace Com.Scm.Fes.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CatDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 上级类别
        /// </summary>
        public long pid { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 图像数量
        /// </summary>
        public int qty { get; set; }
    }
}