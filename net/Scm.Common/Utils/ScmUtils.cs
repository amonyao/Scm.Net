using System;
using System.Text.RegularExpressions;

namespace Com.Scm.Utils
{
    /// <summary>
    /// Scm.Net公共方法
    /// </summary>
    public class ScmUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsValidId(long id)
        {
            return id > 1000;
        }

        public static bool IsNormalId(long id)
        {
            return id > ScmEnv.DEFAULT_ID;
        }

        public static bool IsValidId(string text)
        {
            return Regex.IsMatch(text, @"^[1-9]\d{18}$");
        }

        public static bool IsValidUser(string text)
        {
            return Regex.IsMatch(text, @"^\w{2,32}(@\w{2,32})?$");
        }

        public static bool IsValidPass(string text)
        {
            return Regex.IsMatch(text, @"^\w{64}$");
        }

        public static bool IsValidCode(string text, int len)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z0-9]{" + len + "}$");
        }

        public static bool IsValidCode(string text, int min, int max)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z0-9]{" + min + "," + max + "}$");
        }

        public static bool IsValidUnitCodes(string text)
        {
            return Regex.IsMatch(text, @"^U\w{7}$");
        }

        /// <summary>
        ///  根据文件类型分配路径
        /// </summary>
        /// <param name="fileExt"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string AssigendPath(string fileExt, string path)
        {
            var dataPath = DateTime.Now.ToString("yyyyMMdd");
            if (TextUtils.IsImage(fileExt))
                return path + "/upload/images/" + dataPath + "/";
            if (TextUtils.IsVideo(fileExt))
                return path + "/upload/videos/" + dataPath + "/";
            if (TextUtils.IsDocument(fileExt))
                return "/upload/files/" + dataPath + "/";
            if (TextUtils.IsAudio(fileExt))
                return "/upload/musics/" + dataPath + "/";
            return path + "/upload/others/";
        }
    }
}
