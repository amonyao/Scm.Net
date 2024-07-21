namespace Com.Scm.OAuth.Jd
{
    /// <summary>
    /// 简化模式
    /// </summary>
    public class JdClient2 : JdClient
    {
        private const string SERVICE_URL = "https://oauth.jdl.com/oauth/authorize";

        public override void GetAuth()
        {
            var url = SERVICE_URL;
            url += "?client_id=" + APP_KEY;
            url += "&redirect_uri=urn:ietf:wg:oauth:2.0:oob";
            url += "&response_type=code";

            //try
            //{
            //    Process.Start(url);
            //}
            //catch (Exception ex)
            //{

            //}

            AccessToken = "ce33795598d74d889bfb3094abbb999e";
            RefreshToken = "ad03b2e3f70f4c55bce62601c00489e9";
        }
    }
}
