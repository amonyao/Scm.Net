using Com.Scm.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Express.My.Shipper.BEST
{
    public abstract class BsExpress : MyExpress
    {
        public const string PARTNER_ID = "65605";
        public const string PARTNER_KEY = "3fp8y736lfot";

        #region 公共方法
        public virtual string GetServicePath()
        {
            return "";
        }

        public virtual string GetPartnerId()
        {
            return PARTNER_ID;
        }

        public virtual string GetPartnerKey()
        {
            return PARTNER_KEY;
        }

        private static HashAlgorithm _Md5;
        public string Digest(string json)
        {
            string text = json + GetPartnerKey();

            byte[] buytes = Encoding.UTF8.GetBytes(text);
            if (_Md5 == null)
            {
                _Md5 = MD5.Create();
            }

            buytes = _Md5.ComputeHash(buytes);
            return TextUtils.ToHexString(buytes);
        }
        #endregion
    }
}
