using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class IamLogTicketDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long osp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string ticket { get; set; }
    }
}