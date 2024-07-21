using Com.Scm.Express.CnExpress.ShipperSearch;
using Com.Scm.Express.ShipperSearch;
using Com.Scm.Http;
using Com.Scm.OAuth.Jd;
using Com.Scm.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Express.CnExpress
{
    public class CnExpress
    {
        protected const string APP_KEY = "23b975746df34f9993ea3ba35f68688b";
        protected const string APP_SECRET = "888e64f651384054ba5d25a70874c7c8";

        protected const string BASE_URI = "https://api.jdl.com";
        protected const string BASE_URI_DEV = "https://api.jdl.com";

        private JdClient _JdClient;

        public void Init()
        {
            _JdClient = new JdClient2();
            _JdClient.GetAuth();
        }

        public ShipperSearchResponse ShipperSearch()
        {
            var response = new ShipperSearchResponse();

            var body = "[\"0\"]";

            var cnRequest = new CnShipperSearchRequest();
            var cnResponse = Request<CnShipperSearchResponse>(cnRequest, body);

            return response;
        }

        public string GetAppKey()
        {
            return APP_KEY;
        }

        public string GetAppSecret()
        {
            return APP_SECRET;
        }

        private T Request<T>(CnRequest req, string json) where T : class, new()
        {
            var now = DateTime.Now;
            var timestamp = Timestamp(now);
            var v = "2.0";
            var algorithm = "md5-salt";

            var request = new HttpRequest();
            request.AddQueryParameter("LOP-DN", req.GetDomain());
            request.AddQueryParameter("app_key", GetAppKey());
            request.AddQueryParameter("access_token", _JdClient.GetAccessToken());
            request.AddQueryParameter("timestamp", timestamp);
            request.AddQueryParameter("v", v);
            request.AddQueryParameter("algorithm", algorithm);
            request.AddQueryParameter("sign", Digest(req.GetServicePath(), json, timestamp, v));
            request.AddBody(json);

            var client = new RestClient(BASE_URI + req.GetServicePath());
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
