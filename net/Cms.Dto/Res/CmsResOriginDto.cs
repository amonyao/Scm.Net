using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsResOriginDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long author_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string namee { get; set; }
    }
}