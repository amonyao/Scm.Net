using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsResStyleDto : ScmDataDto
    {
        public const long SYS_ID = 1000000000000000001;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int types { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(128)]
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(2048)]
        public string style { get; set; }
    }
}