using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Utils
{
    public class SecUtils
    {
        public static string Md5(string input)
        {
            if (input == null)
            {
                return null;
            }
            return Md5(Encoding.UTF8.GetBytes(input));
        }

        public static string Md5(Stream stream)
        {
            var alg = MD5.Create();
            byte[] bytes = alg.ComputeHash(stream);
            return TextUtils.ToHexString(bytes);
        }

        public static string Md5(byte[] bytes)
        {
            var alg = MD5.Create();
            bytes = alg.ComputeHash(bytes);
            return TextUtils.ToHexString(bytes);
        }

        public static string Sha(string input)
        {
            var alg = SHA256.Create();
            byte[] bytes = alg.ComputeHash(Encoding.UTF8.GetBytes(input));
            return TextUtils.ToHexString(bytes);
        }

        public static string Sha(Stream stream)
        {
            var alg = SHA256.Create();
            byte[] bytes = alg.ComputeHash(stream);
            return TextUtils.ToHexString(bytes);
        }

        public static string Sha(byte[] bytes)
        {
            var alg = SHA256.Create();
            bytes = alg.ComputeHash(bytes);
            return TextUtils.ToHexString(bytes);
        }
    }
}
