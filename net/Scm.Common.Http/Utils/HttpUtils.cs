using Com.Scm.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Com.Scm.Utils
{
    public partial class HttpUtils
    {
        /// <summary>
        /// 默认用户代理字符串
        /// </summary>
        public static string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66";

        /// <summary>
        /// 内部记录日志
        /// </summary>
        /// <param name="msg"></param>
        private static void DebugLog(string msg)
        {
            LogUtils.Info(msg);
        }

        private static void BuildHeader(HttpClient client, Dictionary<string, string> header, string contentType)
        {
            if (header == null)
            {
                header = new Dictionary<string, string>();
            }

            var key = "Content-Type";
            if (contentType != null)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(key, contentType);
            }

            if (!header.ContainsKey("accept"))
            {
                header.Add("accept", "application/json");
            }
            if (!header.ContainsKey("User-Agent"))
            {
                header.Add("User-Agent", DEFAULT_USER_AGENT);
            }

            if (header.ContainsKey(key))
            {
                contentType = header[key];
                client.DefaultRequestHeaders.TryAddWithoutValidation(key, contentType);
                header.Remove(key);
            }
            foreach (var headerItem in header)
            {
                client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                DebugLog($"GET Header [{headerItem.Key}]=[{headerItem.Value}]");
            }
        }

        #region 异步请求
        /// <summary>
        /// 创建 HttpClient
        /// </summary>
        /// <returns></returns>
        public static HttpClient CreateHttpClient()
        {
            return new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
            });
        }

        /// <summary>
        /// 异步 GET 请求API，返回字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(string url, Dictionary<string, string> query = null, Dictionary<string, string> header = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                url = BuildQuery(query, url, true);
                DebugLog($"GET [{url}]");

                BuildHeader(httpClient, header, null);

                var responseText = await httpClient.GetStringAsync(url);
                DebugLog($"GET [{url}], reponse=[{responseText}]");
                return responseText;
            }
        }

        /// <summary>
        /// 异步 GET 请求API，返回反序列化后的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static async Task<T> GetObjectAsync<T>(string url, Dictionary<string, string> query = null, Dictionary<string, string> header = null) where T : new()
        {
            var result = await GetStringAsync(url, query, header);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }

            return result.AsJsonObject<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private static HttpContent BuildForm(Dictionary<string, string> form)
        {
            if (form == null)
            {
                form = new Dictionary<string, string>();
            }
            RemoveEmpty(form);
            foreach (var item in form)
            {
                DebugLog($"POST [{item.Key}]=[{item.Value}]");
            }

            return new FormUrlEncodedContent(form);
        }

        /// <summary>
        /// 异步 POST 请求API，返回字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static async Task<string> PostFormStringAsync(string url, Dictionary<string, string> form = null, Dictionary<string, string> header = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                DebugLog($"POST [{url}]");
                var content = BuildForm(form);

                BuildHeader(httpClient, header, null);

                var response = await httpClient.PostAsync(url, content);
                var responseText = await response.Content.ReadAsStringAsync();
                DebugLog($"POST [{url}], reponse=[{responseText}]");
                return responseText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="form"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static async Task<T> PostFormObjectAsync<T>(string api, Dictionary<string, string> form = null, Dictionary<string, string> header = null) where T : new()
        {
            var result = await PostFormStringAsync(api, form, header);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }

            return result.AsJsonObject<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static HttpContent BuildJson(string json)
        {
            DebugLog($"POST Json=[{json}]");

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="json"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static async Task<string> PostJsonStringAsync(string api, string json = null, Dictionary<string, string> header = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                DebugLog($"POST [{api}]");
                var content = BuildJson(json);

                BuildHeader(httpClient, header, "application/json; charset=utf-8");

                var message = await httpClient.PostAsync(api, content);
                var responseText = await message.Content.ReadAsStringAsync();
                DebugLog($"POST [{api}], reponse=[{responseText}]");
                return responseText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="json"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static async Task<T> PostJsonObjectAsync<T>(string api, string json = null, Dictionary<string, string> header = null) where T : new()
        {
            var result = await PostJsonStringAsync(api, json, header);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }

            return result.AsJsonObject<T>();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="fileName"></param>
        /// <param name="query"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task DownloadFileAsync(string uri, string fileName, Dictionary<string, string> query = null, Dictionary<string, string> header = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                uri = BuildQuery(query, uri, true);
                DebugLog($"GET [{uri}]");

                BuildHeader(httpClient, header, null);

                // 发送 HTTP 请求以获取文件的响应流 
                using (var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
                {
                    // 检查响应状态码是否表示成功 
                    response.EnsureSuccessStatusCode();

                    // 获取响应流 
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        // 创建本地文件流以写入下载的数据 
                        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            // 将响应流复制到本地文件流 
                            await contentStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }
        #endregion

        #region 同步请求
        /// <summary>
        /// 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string GetString(string url, Dictionary<string, string> query = null, Dictionary<string, string> header = null)
        {
            var request = new ScmHttpRequest(url);
            if (query != null)
            {
                foreach (var item in query)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
            }
            if (header != null)
            {
                foreach (var item in header)
                {
                    if (item.Key.ToLower() == "content-type")
                    {
                        request.ContentType = item.Value;
                        continue;
                    }
                    request.AddHeadParameter(item.Key, item.Value);
                }
            }

            var httpClient = new ScmHttpClient();
            return httpClient.GetText(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static T GetObject<T>(string url, Dictionary<string, string> query = null, Dictionary<string, string> header = null) where T : new()
        {
            var result = GetString(url, query, header);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }

            return result.AsJsonObject<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="form"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string PostFormString(string uri, Dictionary<string, string> form, Dictionary<string, string> header = null)
        {
            var request = new ScmHttpRequest();
            if (form != null)
            {
                foreach (var item in form)
                {
                    request.AddBodyParameter(item.Key, item.Value);
                }
            }
            if (header != null)
            {
                foreach (var item in header)
                {
                    if (item.Key.ToLower() == "content-type")
                    {
                        request.ContentType = item.Value;
                        continue;
                    }
                    request.AddHeadParameter(item.Key, item.Value);
                }
            }

            var httpClient = new ScmHttpClient(uri);
            return httpClient.PostText(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="form"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static T PostFormObject<T>(string uri, Dictionary<string, string> form, Dictionary<string, string> header = null) where T : new()
        {
            var result = PostFormString(uri, form, header);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }

            return result.AsJsonObject<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="json"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string PostJsonString(string uri, string json, Dictionary<string, string> header = null)
        {
            var request = new ScmHttpRequest(uri);
            request.AddBodyContent(json);

            if (header != null)
            {
                foreach (var item in header)
                {
                    if (item.Key.ToLower() == "content-type")
                    {
                        request.ContentType = item.Value;
                        continue;
                    }
                    request.AddHeadParameter(item.Key, item.Value);
                }
            }

            var httpClient = new ScmHttpClient();
            return httpClient.PostText(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="json"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static T PostJsonObject<T>(string uri, string json, Dictionary<string, string> header = null) where T : new()
        {
            var result = PostJsonString(uri, json, header);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }

            return result.AsJsonObject<T>();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 移除空值项
        /// </summary>
        /// <param name="dict"></param>
        public static void RemoveEmpty(Dictionary<string, string> dict)
        {
            dict.Where(item => string.IsNullOrEmpty(item.Value)).Select(item => item.Key).ToList().ForEach(key =>
            {
                dict.Remove(key);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="urlEncode"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string BuildQuery(Dictionary<string, string> paramList, string url = null, bool urlEncode = false)
        {
            if (paramList == null)
            {
                return url;
            }

            var builder = new StringBuilder();
            foreach (var key in paramList.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                var val = paramList[key];
                if (string.IsNullOrEmpty(val))
                {
                    continue;
                }

                builder.Append('&').Append(urlEncode ? UrlEncode(key) : key).Append('=').Append(urlEncode ? UrlEncode(val) : val);
            }
            if (builder.Length > 0)
            {
                builder.Remove(0, 1);
            }

            if (url == null)
            {
                url = "";
            }

            url += url.IndexOf("?") < 0 ? "?" : "&";
            return url + builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Query2Dictionary(string text)
        {
            var dict = new Dictionary<string, string>();
            var kvItems = text.Split('&');
            foreach (var kvItem in kvItems)
            {
                var items = kvItem.Split('=');
                if (items.Length == 2)
                {
                    var key = items[0];
                    var value = items[1];
                    dict.Add(key, value);
                }
            }
            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static T Query2Object<T>(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return default;
            }

            var type = typeof(T);
            var obj = (T)Assembly.GetAssembly(type).CreateInstance(type.FullName);
            if (obj == null)
            {
                return obj;
            }

            var kvItems = text.Split('&');
            foreach (var kvItem in kvItems)
            {
                var items = kvItem.Split('=');
                if (items.Length == 2)
                {
                    var key = items[0];
                    var value = items[1];

                    var p = type.GetProperty(key);
                    if (p != null)
                    {
                        var o = Convert.ChangeType(value, p.PropertyType);
                        p.SetValue(obj, o, null);
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Db2Html(Object obj)
        {
            if (obj == null)
            {
                return "&nbsp;";
            }
            string text = obj.ToString();
            if (text == "")
            {
                return "&nbsp;";
            }
            return text.Replace("&", "&amp;").Replace("  ", "&nbsp;&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Db2Xml(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            string text = obj.ToString().Trim();
            if (text == "")
            {
                return "";
            }
            return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string host = Dns.GetHostName();

            //得到本机IP地址数组
            var ipEntry = Dns.GetHostAddresses(host);
            if (ipEntry != null)
            {
                foreach (var ip in ipEntry)
                {
                    host = ip.ToString();
                    if (IsIPv4(host))
                    {
                        return host;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        public static void GetMacWithIp()
        {
            var list = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var item in list)
            {
                // 检查网络接口的状态
                if (item.OperationalStatus == OperationalStatus.Up && item.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    // 获取MAC地址并进行格式化
                    //string macAddress = string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
                    var mac = BitConverter.ToString(item.GetPhysicalAddress().GetAddressBytes());
                    var ip = item.GetIPProperties().UnicastAddresses
                                .FirstOrDefault(n => n.Address.AddressFamily == AddressFamily.InterNetwork)?
                                .Address.ToString();

                    // 输出网络接口名称和MAC地址
                    LogUtils.Info($"IP: {ip}");
                    LogUtils.Info($"MAC Address: {mac}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static bool IsIPv4(string addr)
        {
            //利用正则表达式判断字符串是否符合IPv4格式
            if (!Regex.IsMatch(addr, "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$"))
            {
                return false;
            }

            //根据小数点分拆字符串
            foreach (var ip in addr.Split('.'))
            {
                if (int.Parse(ip) > 255)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 编码转换
        /// <summary>
        /// unescape
        /// </summary>
        /// <returns></returns>
        public static string Unescape(string s)
        {
            string str = s.Remove(0, 2);//删除最前面两个＂%u＂
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串＂%u＂分隔
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
            {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16);  //把十六进制形式的字串符串转换为二进制字节
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr); //把字节转为unicode编码
            return str;
        }

        /// <summary>
        /// Escape
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Escape(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                sb.Append((char.IsLetterOrDigit(c)
                || c == '-' || c == '_' || c == '\\'
                || c == '/' || c == '.') ? c.ToString() : Uri.HexEscape(c));
            }
            return sb.ToString();
        }

        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return System.Web.HttpUtility.UrlEncode(str, Encoding.UTF8);
        }

        public static string ToBase64String(byte[] input)
        {
            return Convert.ToBase64String(input).Replace("+", "-").Replace("/", "_");
        }

        public static byte[] FromBase64String(string input)
        {
            return Convert.FromBase64String(input.Replace("-", "+").Replace("_", "/"));
        }
        #endregion
    }
}
