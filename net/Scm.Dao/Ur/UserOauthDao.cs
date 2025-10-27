using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Ur
{
    /// <summary>
    /// 三方登录
    /// </summary>
    [SugarTable("scm_ur_user_oauth")]
    public class UserOAuthDao : ScmDataDao
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        public long user_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 外部应用
        /// </summary>
        public string provider { get; set; }

        /// <summary>
        /// 授权ID
        /// </summary>
        public string oauth_id { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int qty { get; set; }
    }
}