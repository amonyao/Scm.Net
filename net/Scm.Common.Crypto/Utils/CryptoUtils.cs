using Org.BouncyCastle.Crypto.Digests;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Com.Scm.Utils
{
    public class CryptoUtils
    {
        private static readonly string Default_AES_Key = "@#kim123";

        private static byte[] Keys = new byte[16]
        {
            65, 114, 101, 121, 111, 117, 109, 121, 83, 110,
            111, 119, 109, 97, 110, 63
        };

        public static string AesEncrypt(string encryptString)
        {
            return AesEncrypt(encryptString, Default_AES_Key);
        }

        public static string AesEncrypt(string encryptString, string encryptKey, CipherMode mode = CipherMode.CBC)
        {
            //return text.AESEncrypt();
            //encryptKey = encryptKey.GetSubString(32, "");
            encryptKey = encryptKey.PadRight(32, ' ');
            var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            aes.IV = Keys;
            aes.Mode = mode;
            var cryptoTransform = aes.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string Md5(string input)
        {
            if (input == null)
            {
                return null;
            }

            return Md5(Encoding.UTF8.GetBytes(input));
        }

        public static string Md5(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            var alg = MD5.Create();
            bytes = alg.ComputeHash(bytes);
            return TextUtils.ToHexString(bytes);
        }

        public static string Md5(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            var alg = MD5.Create();
            byte[] bytes = alg.ComputeHash(stream);
            return TextUtils.ToHexString(bytes);
        }

        public static string Sha(string input)
        {
            if (input == null)
            {
                return null;
            }

            return Sha(Encoding.UTF8.GetBytes(input));
        }

        public static string Sha(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            var alg = SHA256.Create();
            bytes = alg.ComputeHash(bytes);
            return TextUtils.ToHexString(bytes);
        }

        public static string Sha(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            var alg = SHA256.Create();
            var bytes = alg.ComputeHash(stream);
            return TextUtils.ToHexString(bytes);
        }

        public static string Digest(string name, string text)
        {
            var digest = new Sha1Digest();
            var resBuf = new byte[digest.GetDigestSize()];
            var input = Encoding.UTF8.GetBytes(text);

            digest.BlockUpdate(input, 0, input.Length);
            digest.DoFinal(resBuf, 0);

            return TextUtils.ToHexString(resBuf);
        }
    }
}