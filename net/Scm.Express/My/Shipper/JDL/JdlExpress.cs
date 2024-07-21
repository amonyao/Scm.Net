using Com.Scm.Express.Dto;
using Com.Scm.Http;
using Com.Scm.OAuth.Jd;
using Com.Scm.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Express.My.Shipper.JDL
{
    public abstract class JdlExpress : MyExpress
    {
        protected const string APP_KEY = "23b975746df34f9993ea3ba35f68688b";
        protected const string APP_SECRET = "888e64f651384054ba5d25a70874c7c8";

        protected const string APP_KEY_DEV = "19a6a2043fbb47aab02f486bef654ab7";
        protected const string APP_SECRET_DEV = "bafbc6db027d496da35dcb7b5d8ade59";

        protected JdClient _JdClient;

        public string GetServiceUrl()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return "https://api.jdl.com";
            }
            return "https://test-api.jdl.cn";
        }

        public string GetAppKey()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return APP_KEY;
            }
            return APP_KEY_DEV;
        }

        public string GetAppSecret()
        {
            if (Environment == EnvironmentEnum.Release)
            {
                return APP_SECRET;
            }
            return APP_SECRET_DEV;
        }

        protected T Request<T>(JdlRequest req, string json) where T : class, new()
        {
            var now = DateTime.Now;
            var timestamp = Timestamp(now);
            var v = "2.0";
            var algorithm = "md5-salt";

            var request = new HttpRequest();
            var domain = req.GetDomain();
            if (!string.IsNullOrWhiteSpace(domain))
            {
                request.AddQueryParameter("LOP-DN", req.GetDomain());
            }
            request.AddQueryParameter("app_key", GetAppKey());
            request.AddQueryParameter("access_token", _JdClient.GetAccessToken());
            request.AddQueryParameter("timestamp", timestamp);
            request.AddQueryParameter("v", v);
            request.AddQueryParameter("algorithm", algorithm);
            request.AddQueryParameter("sign", Digest(req.GetPath(), json, timestamp, v));
            request.AddBody(json);

            var client = new RestClient(GetServiceUrl() + req.GetPath());
            client.AddDefaultHeader("Content-type", "application/json;charset=utf-8");

            var text = client.Post(request);
            return text.AsJsonObject<T>();
        }

        private HashAlgorithm _Md5;
        public string Digest(string path, string json, string timestamp, string v)
        {
            string text = GetAppSecret();
            text += "access_token" + _JdClient.GetAccessToken();
            text += "app_key" + GetAppKey();
            text += "method" + path;
            text += "param_json" + json;
            text += "timestamp" + timestamp;
            text += "v" + v;
            text += GetAppSecret();

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            bytes = _Md5.ComputeHash(bytes);
            //return CharUtil.ToHexString(buytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static string Timestamp(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
