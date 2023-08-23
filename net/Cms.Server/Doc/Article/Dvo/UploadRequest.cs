using Com.Scm.Dvo;
using Microsoft.AspNetCore.Http;

namespace Com.Scm.Cms.Doc.Article.Dvo
{
    public class UploadRequest : ScmRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public IFormFile file { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filename { get; set; }
    }
}
