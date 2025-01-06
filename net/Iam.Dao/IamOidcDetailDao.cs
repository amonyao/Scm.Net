using Com.Scm.Dao;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    /// <summary>
    /// 服务商维度的OAuth
    /// </summary>
    [SugarTable("iam_oidc_detail")]
    public class IamOidcDetailDao : ScmDataDao
    {
        /// <summary>
        /// 服务端ID
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 服务端ID
        /// </summary>
        public long osp_id { get; set; }

        /// <summary>
        /// OIDC代码
        /// </summary>
        [StringLength(64)]
        public string code { get; set; }

        /// <summary>
        /// 授权代码
        /// </summary>
        [StringLength(128)]
        public string oauth_code { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        [StringLength(64)]
        public string user { get; set; }

        /// <summary>
        /// 展示姓名
        /// </summary>
        [StringLength(64)]
        public string name { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [StringLength(64)]
        public string avatar { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(32)]
        public string phone { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(32)]
        public string email { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 访问令牌(服务商)
        /// </summary>
        [StringLength(128)]
        public string access_token { get; set; }

        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 刷新令牌(服务商)
        /// </summary>
        [StringLength(128)]
        public string refresh_token { get; set; }

        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        public long refresh_expires { get; set; }

        public bool IsAccessExpired(DateTime time)
        {
            return TimeUtils.GetUnixTime(time) > access_expires;
        }

        public bool IsRefreshExpired(DateTime time)
        {
            return TimeUtils.GetUnixTime(time) > access_expires;
        }
    }
}