using Com.Scm.Dvo;
using Com.Scm.OAuth.Web.Dto;

namespace Com.Scm.Iam.Res.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class IamResAppDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string app_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string app_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string app_desc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string redirect_uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string app_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string app_secret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long refresh_expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OspOrderByEnum order_by { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OspShowMoreEnum show_more { get; set; }
    }
}