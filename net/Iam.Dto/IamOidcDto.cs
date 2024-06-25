using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam
{
    /// <summary>
    /// 
    /// </summary>
    public class IamOidcDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long osp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string oauth_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string user { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string refresh_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long refresh_expires { get; set; }
    }
}