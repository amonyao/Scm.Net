using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    public class IamUserOAuthDto : ScmDataDto
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
        /// 用户代码
        /// </summary>
        [StringLength(64)]
        public string code { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        [StringLength(64)]
        public string oauth_code { get; set; }
    }
}
