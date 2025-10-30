namespace Com.Scm.Utils
{
    public static class TextExts
    {
        #region 格式转换
        public static string ToJsonString(this object obj, bool indented = false, bool ignore = false)
        {
            return TextUtils.ToJsonString(obj, indented, ignore);
        }

        public static T AsJsonObject<T>(this string text) where T : new()
        {
            return TextUtils.AsJsonObject<T>(text);
        }

        public static string ToXmlString(this object obj)
        {
            return TextUtils.ToXmlString(obj);
        }

        public static T AsXmlObject<T>(this string text) where T : new()
        {
            return TextUtils.AsXmlObject<T>(text);
        }

        public static T ConvertTo<T>(this object src, bool props = true, bool fields = true)
        {
            return TextUtils.ConvertTo<T>(src, props, fields);
        }
        #endregion

        #region 数值转换
        public static string ToWeight(this int weight)
        {
            return TextUtils.FormatWeight(weight);
        }

        public static int AsWeight(this string volume)
        {
            return TextUtils.ParseWeight(volume);
        }

        public static string ToVolume(this int volume)
        {
            return TextUtils.FormatVolume(volume);
        }

        public static int AsVolume(this string volume)
        {
            return TextUtils.ParseVolume(volume);
        }

        public static string ToPrice(this int price)
        {
            return TextUtils.FormatPrice(price);
        }

        public static int AsPrice(this string price)
        {
            return TextUtils.ParsePrice(price);
        }
        #endregion

        /// <summary>
        /// 取左侧不超指定长度的字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length">最大长度</param>
        /// <param name="append">超出时，追加提示字符串</param>
        /// <returns></returns>
        public static string Left(this string text, int length, string append = "")
        {
            return TextUtils.Left(text, length, append);
        }

        /// <summary>
        /// 取左侧指定长度的字符，不足时以指定字符填充，超过时附加指定字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length">长度</param>
        /// <param name="pad">不足时，固定填充字符</param>
        /// <param name="append">超出时，追加提示字符串</param>
        /// <returns></returns>
        public static string Left(this string text, int length, char pad, string append = "")
        {
            return TextUtils.Left(text, length, pad, append);
        }

        /// <summary>
        /// 取右侧不超指定长度的字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length">最大长度</param>
        /// <param name="prepend">超出时，追加提示字符串</param>
        /// <returns></returns>
        public static string Right(this string text, int length, string prepend = "")
        {
            return TextUtils.Right(text, length, prepend);
        }

        /// <summary>
        /// 取右侧指定长度的字符，不足时以指定字符填充，超过时附加指定字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length">长度</param>
        /// <param name="pad">不足时，固定填充字符</param>
        /// <param name="prepend">超出时，追加提示字符串</param>
        /// <returns></returns>
        public static string Right(this string text, int length, char pad, string prepend = "")
        {
            return TextUtils.Right(text, length, pad, prepend);
        }

        /// <summary>
        /// 字符串掩码，含手机号、用户名等
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="mask">掩码符</param>
        /// <returns></returns>
        public static string Mask(this string s, char mask = '*')
        {
            return TextUtils.Mask(s, mask);
        }

        public static string FirstCharToLower(this string s)
        {
            return TextUtils.FirstCharToLower(s);
        }

        public static string FirstCharToUpper(this string s)
        {
            return TextUtils.FirstCharToUpper(s);
        }

        public static string FilterEmoji(this string str)
        {
            return TextUtils.FilterEmoji(str);
        }
    }
}
