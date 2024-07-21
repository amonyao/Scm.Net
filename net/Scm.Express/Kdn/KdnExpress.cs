using Com.Scm.Express.Kdn.RouteSearch;
using Com.Scm.Express.ShipperSearch;
using Com.Scm.Http;
using Com.Scm.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Express.Kdn
{
    /// <summary>
    /// 快递鸟
    /// </summary>
    public class KdnExpress
    {
        protected const string USER_ID = "1597520";
        protected const string API_KEY = "cf1c5639-907f-492f-bd8a-382a9adeb717";

        public void Init()
        {
        }

        public ShipperSearchResponse ShipperSearch()
        {
            var response = new ShipperSearchResponse();
            response.Append(new ShipperSearchResult { code = "HTKY", name = "百世快递" });
            response.Append(new ShipperSearchResult { code = "ZTO", name = "中通快递" });
            response.Append(new ShipperSearchResult { code = "STO", name = "申通快递" });
            response.Append(new ShipperSearchResult { code = "YTO", name = "圆通速递" });
            response.Append(new ShipperSearchResult { code = "YD", name = "韵达速递" });
            response.Append(new ShipperSearchResult { code = "YZPY", name = "邮政快递包裹" });
            response.Append(new ShipperSearchResult { code = "EMS", name = "EMS" });
            response.Append(new ShipperSearchResult { code = "SF", name = "顺丰速运" });
            response.Append(new ShipperSearchResult { code = "JD", name = "京东快递" });
            response.Append(new ShipperSearchResult { code = "JTSD", name = "极兔速递" });

            return response;
        }

        public KdnResponse RouteSearch(KdnRouteSearchRequest req)
        {
            return Request<KdnRouteSearchResponse>(req);
        }

        private T Request<T>(KdnRequest req) where T : class, new()
        {
            var json = req.ToJsonString();

            var request = new HttpRequest();
            request.AddQueryParameter("RequestData", json);
            request.AddQueryParameter("EBusinessID", USER_ID);
            request.AddQueryParameter("RequestType", req.GetRequestType());
            request.AddQueryParameter("DataType", "2");
            request.AddQueryParameter("DataSign", Digest(json));
            request.AddBody(json);

            var client = new RestClient(req.GetServicePath());
            client.AddDefaultHeader("Content-type", "application/x-www-form-urlencoded;charset=utf-8");

            var text = client.Post(request);
            return text.AsJsonObject<T>();
        }

        private HashAlgorithm _Md5;
        public string Digest(string json)
        {
            string text = json + API_KEY;

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            bytes = _Md5.ComputeHash(bytes);
            var digest = TextUtils.ToHexString(bytes);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(digest));
        }
    }
}
