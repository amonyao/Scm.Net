using Com.Scm.Dvo;

namespace Com.Scm.Cms.Doc.Article.Dvo
{
    public class UploadResponse : ScmResponse
    {
        public string file { get; set; }
        /// <summary>
        /// TinyMCE专用
        /// </summary>
        public string location { get; set; }
    }
}
