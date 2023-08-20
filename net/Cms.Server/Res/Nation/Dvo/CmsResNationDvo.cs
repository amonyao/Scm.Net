using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 国别
    /// </summary>
    public class CmsResNationDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public int od { get; set; } = 0;

        /// <summary>
        /// 编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string namec { get; set; }
    }
}