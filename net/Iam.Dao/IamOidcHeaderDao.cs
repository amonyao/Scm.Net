using Com.Scm.Dao;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    /// <summary>
    /// 用户App维度的OAuth
    /// </summary>
    [SugarTable("iam_oidc_header")]
    public class IamOidcHeaderDao : ScmDataDao
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public long app_id { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        [StringLength(64)]
        public string code { get; set; }

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
        /// 登录次数
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 访问令牌（基于用户及AppID）
        /// </summary>
        [StringLength(128)]
        public string access_token { get; set; }

        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 刷新令牌
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
