using System.Text;

namespace Com.Scm.Http
{
    public class ScmHttpResponse
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 默认字符集
        /// </summary>
        public Encoding Encoding = Encoding.UTF8;
    }
}
