using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Iam.Log
{
    public class IamLogAppTicketDto : ScmDataDto
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
