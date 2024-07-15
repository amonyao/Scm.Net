using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Cfg
{
    /// <summary>
    /// 
    /// </summary>
    public class IamCfgAppOspDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long app_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long osp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(100)]
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(100)]
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}