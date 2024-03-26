using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 
    /// </summary>
    public class FileHandleDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int handle { get; set; }
    }
}