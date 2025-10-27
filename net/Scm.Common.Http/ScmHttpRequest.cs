using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Com.Scm.Http
{
    public class ScmHttpRequest
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 请求头参数
        /// </summary>
        public readonly List<ScmHttpParam> HeadParams = new List<ScmHttpParam>();
        /// <summary>
        /// 请求体参数
        /// </summary>
        public readonly List<ScmHttpParam> BodyParams = new List<ScmHttpParam>();
        /// <summary>
        /// 请求内容
        /// </summary>
        public string BodyContent { get; set; }

        /// <summary>
        /// 是否需要进行参数转换
        /// </summary>
        public bool EncodeContent { get; set; }
        /// <summary>
        /// 默认字符集
        /// </summary>
        public Encoding Encoding = Encoding.UTF8;

        public ScmHttpRequest()
        {
        }

        public ScmHttpRequest(string url)
        {
            Url = url;
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddQueryParameter(string name, string value)
        {
            BodyParams.Add(new ScmHttpParam { IsGet = true, Key = name, Value = value });
        }

        /// <summary>
        /// 请求体参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isGet"></param>
        public void AddBodyParameter(string name, string value, bool isGet = false)
        {
            BodyParams.Add(new ScmHttpParam { IsGet = isGet, Key = name, Value = value });
        }

        /// <summary>
        /// 请求体参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isGet"></param>
        public void AddBodyParameter(string name, long value, bool isGet = false)
        {
            BodyParams.Add(new ScmHttpParam { IsGet = isGet, Key = name, Value = value.ToString() });
        }

        /// <summary>
        /// 请求头参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddHeadParameter(string name, long value)
        {
            HeadParams.Add(new ScmHttpParam { Key = name, Value = value.ToString() });
        }

        /// <summary>
        /// 请求头参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddHeadParameter(string name, string value)
        {
            HeadParams.Add(new ScmHttpParam { Key = name, Value = value });
        }

        /// <summary>
        /// 请求体内容
        /// </summary>
        /// <param name="body"></param>
        public void AddBodyContent(string body)
        {
            BodyContent = body;
        }

        /// <summary>
        /// 参数转码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string UrlEncode(string text)
        {
            return EncodeContent ? HttpUtility.UrlEncode(text) : text;
        }

        /// <summary>
        /// 构建参数
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns>构建后的URL地址</returns>
        public string Build(string baseUrl)
        {
            var hasBody = !string.IsNullOrWhiteSpace(BodyContent);

            var getBuffer = new StringBuilder();
            var postBuffer = new StringBuilder();

            if (BodyParams != null && BodyParams.Count > 0)
            {
                foreach (var param in BodyParams)
                {
                    if (hasBody || param.IsGet)
                    {
                        getBuffer.Append(param.Key).Append("=").Append(param.Value).Append('&');
                        continue;
                    }

                    postBuffer.Append(UrlEncode(param.Key)).Append("=").Append(UrlEncode(param.Value)).Append('&');
                }

                if (getBuffer.Length > 0)
                {
                    getBuffer = getBuffer.Remove(getBuffer.Length - 1, 1);
                }
                if (postBuffer.Length > 0)
                {
                    postBuffer = postBuffer.Remove(postBuffer.Length - 1, 1);
                }
            }

            var url = GetUrl(baseUrl);
            if (getBuffer.Length > 0)
            {
                url = url + (Url.IndexOf('?') > 0 ? '&' : '?') + getBuffer.ToString();
            }

            if (!hasBody)
            {
                BodyContent = postBuffer.ToString();
            }

            return url;
        }

        private string GetUrl(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return Url;
            }
            if (Url.IndexOf("//") > 0)
            {
                return Url;
            }
            if (baseUrl.EndsWith("/"))
            {
                baseUrl.Substring(0, baseUrl.Length - 1);
            }
            if (!Url.StartsWith("/"))
            {
                Url = "/" + Url;
            }
            return baseUrl + Url;
        }

        public byte[] GetContent()
        {
            return Encoding.GetBytes(BodyContent);
        }
    }
}
