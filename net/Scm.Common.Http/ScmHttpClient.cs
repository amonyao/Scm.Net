using Com.Scm.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Com.Scm.Http
{
    public class ScmHttpClient
    {
        public const string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66";

        private const string KEY_CONTENT_TYPE = "Content-Type";

        /// <summary>
        /// 服务路径
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; } = DEFAULT_USER_AGENT;
        /// <summary>
        /// 超时时长
        /// </summary>
        public int Timeout { get; set; } = 3000;
        /// <summary>
        /// Cookies
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// 请求头参数（全局）
        /// </summary>
        private List<ScmHttpParam> _HeadParams = new List<ScmHttpParam>();

        public ScmHttpClient()
        {
        }

        public ScmHttpClient(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        public void AddHeadParameter(string name, string value)
        {
            _HeadParams.Add(new ScmHttpParam { Key = name, Value = value });
        }

        private HttpWebRequest CreateRequest(ScmHttpRequest request)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            var url = request.Build(BaseUrl);

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ProtocolVersion = HttpVersion.Version11;
            webRequest.AllowAutoRedirect = true;

            #region 设置Header
            if (_HeadParams.Count > 0)
            {
                foreach (var param in _HeadParams)
                {
                    var key = param.Key?.Trim();
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }
                    if (KEY_CONTENT_TYPE.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    webRequest.Headers.Add(key, param.Value);
                }
            }
            webRequest.ContentType = request.ContentType;
            #endregion

            #region 设置代理UserAgent
            if (!string.IsNullOrWhiteSpace(UserAgent))
            {
                webRequest.UserAgent = UserAgent;
            }
            #endregion

            #region 设置超时
            if (Timeout > 0)
            {
                webRequest.Timeout = Timeout;
            }
            #endregion

            #region 设置Cookie
            if (Cookies != null)
            {
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.Add(Cookies);
            }
            #endregion

            return webRequest;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //如果不希望验证证书有效性，直接返回 true
        }

        /// <summary>
        /// 同步读取字节（Get）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public byte[] GetByte(ScmHttpRequest request)
        {
            var webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.GET;

            return ReadByte(webRequest);
        }

        /// <summary>
        /// 同步读取文本（Get）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetText(ScmHttpRequest request, Encoding encoding = null)
        {
            var webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.GET;

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return ReadText(webRequest, encoding);
        }

        /// <summary>
        /// 同步读取字节（Post）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public byte[] PostByte(ScmHttpRequest request)
        {
            HttpWebRequest webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.POST;

            var contentType = request.ContentType;
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = HttpContentType.WWW_FORM_URLENCODED;
            }
            webRequest.ContentType = contentType;

            SendData(webRequest, request.GetContent());

            return ReadByte(webRequest);
        }

        /// <summary>
        /// 同步读取文本（Post）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string PostText(ScmHttpRequest request, Encoding encoding = null)
        {
            var webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.POST;

            var contentType = request.ContentType;
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = HttpContentType.WWW_FORM_URLENCODED;
            }
            webRequest.ContentType = contentType;

            SendData(webRequest, request.GetContent());

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return ReadText(webRequest, encoding);
        }

        private void SendData(HttpWebRequest request, byte[] data)
        {
            if (data == null || data.Length < 1)
            {
                return;
            }

            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 同步读取字节
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private byte[] ReadByte(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var bytes = new byte[response.ContentLength];
                int length = stream.Read(bytes, 0, bytes.Length);

                var result = bytes;
                if (length != bytes.Length)
                {
                    result = new byte[length];
                    Array.Copy(bytes, result, length);
                }
                return result;
            }
        }

        /// <summary>
        /// 同步读取文本
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private string ReadText(HttpWebRequest request, Encoding encoding)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    var result = reader.ReadToEnd();
                    LogUtils.Info("ReadText:" + result);
                    return result;
                }
            }
        }

        /// <summary>
        /// 异步读取字节（Get）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<byte[]> GetByteAsync(ScmHttpRequest request)
        {
            var webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.GET;

            return await ReadByteAsync(webRequest);
        }

        /// <summary>
        /// 异步读取文本（Get）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetTextAsync(ScmHttpRequest request, Encoding encoding = null)
        {
            var webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.GET;

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return await ReadTextAsync(webRequest, encoding);
        }

        /// <summary>
        /// 异步读取字节（Post）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<byte[]> PostByteAsync(ScmHttpRequest request)
        {
            HttpWebRequest webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.POST;

            await SendDataAsync(webRequest, request.BodyContent);

            return await ReadByteAsync(webRequest);
        }

        /// <summary>
        /// 异步读取文本（Post）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> PostTextAsync(ScmHttpRequest request, Encoding encoding = null)
        {
            var webRequest = CreateRequest(request);
            webRequest.Method = HttpMethodType.POST;

            await SendDataAsync(webRequest, request.BodyContent);

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return await ReadTextAsync(webRequest, encoding);
        }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private async Task SendDataAsync(HttpWebRequest request, string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(body);
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }
        }

        private async Task<byte[]> ReadByteAsync(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var bytes = new byte[response.ContentLength];
                int length = await stream.ReadAsync(bytes, 0, bytes.Length);

                var result = bytes;
                if (length != bytes.Length)
                {
                    result = new byte[length];
                    Array.Copy(bytes, result, length);
                }
                return result;
            }
        }

        private async Task<string> ReadTextAsync(HttpWebRequest request, Encoding encoding)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    var result = await reader.ReadToEndAsync();
                    LogUtils.Info("ReadTextAsync:" + result);
                    return result;
                }
            }
        }
    }
}
