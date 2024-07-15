using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Log
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("iam_log_ticket")]
    public class IamLogTicketDao : ScmDataDao
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