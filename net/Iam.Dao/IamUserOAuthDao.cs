using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    [SugarTable("iam_user_oauth")]
    public class IamUserOAuthDao : ScmDataDao
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public long osp_id { get; set; }

        /// <summary>
        /// 授权代码
        /// </summary>
        [StringLength(128)]
        public string oauth_code { get; set; }
    }
}
