using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Res
{
    /// <summary>
    /// 
    /// </summary>
    public class TagDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}