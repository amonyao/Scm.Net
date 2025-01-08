using Com.Scm.Dao;
using Com.Scm.Enums;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Log
{
    /// <summary>
    /// 三方登录
    /// </summary>
    [SugarTable("iam_log_oauth")]
    public class IamLogOauthDao : ScmDataDao
    {
        /// <summary>
        /// 登录标识
        /// </summary>
        [Required]
        [StringLength(64)]
        public string ticket { get; set; }

        /// <summary>
        /// 请求ID
        /// </summary>
        [StringLength(32)]
        public string request_id { get; set; }

        /// <summary>
        /// 授权ID（应用）
        /// </summary>
        public long oidc_header_id { get; set; }

        /// <summary>
        /// 授权ID（服务商）
        /// </summary>
        public long oidc_detail_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string response_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string redirect_uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string state { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string scope { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public long app_id { get; set; }

        /// <summary>
        /// 登录网站
        /// </summary>
        [Required]
        public long osp_id { get; set; }

        /// <summary>
        /// UnionID
        /// </summary>
        [StringLength(128)]
        public string oauth_code { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [StringLength(64)]
        public string user { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(32)]
        public string name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public ScmSexEnum sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string refresh_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Required]
        public long expires_in { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [StringLength(8)]
        public string err_code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [StringLength(128)]
        public string err_msg { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string email { get; set; }

        public OAuthHandleEnum handle { get; set; }

        public OAuthResultEnum result { get; set; }

        public bool IsExpired(DateTime time)
        {
            return TimeUtils.GetUnixTime(time) > expires_in;
        }
    }
}