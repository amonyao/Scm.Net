using Com.Scm.Dvo;

namespace Com.Scm.Iam.Dvo
{
    /// <summary>
    /// 
    /// </summary>
    public class IamOidcDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long osp_id { get; set; }
        public string osp_code { get; set; }
        public string osp_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oauth_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long refresh_expires { get; set; }
    }
}