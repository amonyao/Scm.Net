using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 国别
    /// </summary>
    public class CmsResNationDto : ScmDataDto
    {
        public const long SYS_ID = 1000000000000000001;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }
    }
}