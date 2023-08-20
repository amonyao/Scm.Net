using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsResTagDvo : ScmDataDvo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int types { get; set; } = 0;

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        public int qty { get; set; } = 0;
    }
}