using Com.Scm.Utils;

namespace Com.Scm.Iam
{
    public static class OidcUtils
    {
        public static string GenAppKey()
        {
            return DateTime.UtcNow.Ticks.ToString("x16");
        }

        public static string GenAppSecret()
        {
            return TextUtils.RandomString(32, false);
        }
    }
}
