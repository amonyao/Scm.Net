using Com.Scm.Express.Dto;
using Com.Scm.Express.My.Shipper.SF.Exp.Auth;
using Com.Scm.Http;
using Com.Scm.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Express.My.Shipper.SF
{
    public abstract class SfExpress : MyExpress
    {
        public const string CUSTOMER_CODE = "YCWLWKJ";
        public const string CUSTOMER_PASS = "pNGDF9JxcEjbalYbKRr8yKVktnDFL3JM";

        protected string _AccessToken = "";

        #region 公共方法
        /// <summary>
        /// 获取授权
        /// </summary>
        /// <returns></returns>
        public bool GetAuth()
        {
            if (!string.IsNullOrWhiteSpace(_AccessToken))
            {
                return true;
            }

            string url;
            if (Environment == EnvironmentEnum.Release)
            {
                url = "https://sfapi.sf-express.com/oauth2/accessToken";
            }
            else
            {
                url = "https://sfapi-sbox.sf-express.com/oauth2/accessToken";
            }

            var req = new ScmHttpRequest();
            req.AddQueryParameter("partnerID", CUSTOMER_CODE);
            req.AddQueryParameter("secret", CUSTOMER_PASS);
            req.AddQueryParameter("grantType", "password");
            var response = Post<AuthResponse>(url, req);
            if (response == null || !response.IsSuccess())
            {
                return false;
            }

            _AccessToken = response.accessToken;
            return true;
        }

        protected static T Post<T>(string url, ScmHttpRequest request) where T : class, new()
        {
            var client = new ScmHttpClient(url);
            client.AddHeadParam("Content-type", "application/x-www-form-urlencoded;charset=UTF-8");
            var text = client.PostText(request);
            return text.AsJsonObject<T>();
        }

        public long Timestamp(DateTime time)
        {
            return (long)(time - _UnixTime).TotalSeconds; // 相差秒数
        }

        private static HashAlgorithm _Md5;
        protected static string Digest(string json, long timestamp)
        {
            var text = json + timestamp + CUSTOMER_PASS;
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            text = UrlEncode(text, Encoding.UTF8);
            var bytes = _Md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(bytes);
        }
        #endregion
    }
}
