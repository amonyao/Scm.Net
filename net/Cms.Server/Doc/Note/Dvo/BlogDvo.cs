using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Note.Dvo
{
    public class BlogDvo : ScmDataDvo
    {
        /// <summary>
        /// 分类
        /// </summary>
        public long cat_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sub_title { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string summary { get; set; }
    }
}
