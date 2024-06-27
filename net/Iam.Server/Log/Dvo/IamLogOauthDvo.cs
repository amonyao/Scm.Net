using Com.Scm.Dvo;
using Com.Scm.Enums;

namespace Com.Scm.Iam.Log.Dvo
{
    /// <summary>
    /// 三方登录
    /// </summary>
    public class IamLogOauthDvo : ScmDataDvo
    {
        /// <summary>
        /// 登录标识
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long oidc_id { get; set; }
        public string oidc_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long app_id { get; set; }
        public string app_code { get; set; }
        public string app_name { get; set; }

        /// <summary>
        /// 登录网站
        /// </summary>
        public long osp_id { get; set; }
        public string osp_code { get; set; }
        public string osp_name { get; set; }

        /// <summary>
        /// UnionID
        /// </summary>
        public string oauth_id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ScmSexEnum sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long expires_in { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string err_msg { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
    }
}