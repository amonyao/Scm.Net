using Com.Scm.Dto;
using Com.Scm.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class IamResOspDto : ScmDataDto
    {
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
        [Required]
        public int ver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string icon { get; set; }

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