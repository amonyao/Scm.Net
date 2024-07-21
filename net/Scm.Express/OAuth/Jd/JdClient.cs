namespace Com.Scm.OAuth.Jd
{
    public class JdClient
    {
        protected const string APP_KEY = "23b975746df34f9993ea3ba35f68688b";
        protected const string APP_SECRET = "888e64f651384054ba5d25a70874c7c8";
        protected const string RSA_ID = "3057";
        protected const string RSA_PUBLIC = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCCctQgHoixgKq9VaL3mPHWLWLIC5z7bOpNv6i4yupxw6PZaVQsdaelda/IKWAK1ScN0Sr/M6iKcklRJ89gH1fufs1yg8HlHRb7habOuZvW5wL1jylaS9/matNQdZVlzJy6F7NsSNc/8Bf0g9Az+kXGax87McxEVS7eiYFsWiHpFQIDAQAB";

        protected string AccessToken = "";
        protected string RefreshToken = "";

        public virtual void GetAuth()
        {

        }

        public string GetAppKey()
        {
            return APP_KEY;
        }

        public string GetAppSecret()
        {
            return APP_SECRET;
        }

        public string GetAccessToken()
        {
            return AccessToken;
        }

        public string GetRefreshToken()
        {
            return RefreshToken;
        }
    }
}
