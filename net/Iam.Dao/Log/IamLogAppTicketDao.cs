using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Log
{
    [SugarTable("iam_log_app_ticket")]
    public class IamLogAppTicketDao : ScmDataDao
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [Required]
        public long app_id { get; set; }

        /// <summary>
        /// 请求标识
        /// </summary>
        public string seq { get; set; }

        /// <summary>
        /// 握手票据
        /// </summary>
        [StringLength(32)]
        public string ticket { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public AppTicketHandleEnum handle { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public AppTicketResultEnum result { get; set; }
    }
}
