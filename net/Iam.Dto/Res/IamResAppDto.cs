using Com.Scm.Dto;
using Com.Scm.OAuth.Web.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class IamResAppDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string app_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string app_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string app_desc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string redirect_uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string app_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(64)]
        public string app_secret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string refresh_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long access_expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long refresh_expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int max { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public OspOrderByEnum order_by { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public OspShowMoreEnum show_more { get; set; }
    }
}