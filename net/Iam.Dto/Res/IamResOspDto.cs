using Com.Scm.Dto;
using Com.Scm.Enums;
using Com.Scm.OAuth.Web.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class IamResOspDto : ScmDataDto
    {
        public const long PHONE_ID = 1000000000000001010;
        public const string PHONE_CODE = "Phone";
        public const long EMAIL_ID = 1000000000000001020;
        public const string EMAIL_CODE = "Email";

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string icon { get; set; }

        /// <summary>
        /// ·þÎñµØÖ·
        /// </summary>
        [StringLength(128)]
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public OspVerEnum ver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public ScmSystemEnum row_system { get; set; }
    }
}