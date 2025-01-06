using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    public class IamOidcHeaderDto : ScmDataDto
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
        /// 访问Token
        /// </summary>
        [StringLength(128)]
        public string access_token { get; set; }

        /// <summary>
        /// 访问Token过期时间
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 刷新Token
        /// </summary>
        [StringLength(128)]
        public string refresh_token { get; set; }

        /// <summary>
        /// 刷新Token过期时间
        /// </summary>
        public long refresh_expires { get; set; }
    }
}
