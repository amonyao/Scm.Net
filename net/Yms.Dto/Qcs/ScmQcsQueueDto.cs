using Com.Scm.Dto;
using Com.Scm.Yms;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Qcs
{
    /// <summary>
    /// 号码
    /// </summary>
    public class ScmQcsQueueDto : ScmDataDto
    {
        /// <summary>
        /// 方案ID
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 队列ID
        /// </summary>
        public long detail_id { get; set; }

        /// <summary>
        /// 排队序号
        /// </summary>
        [Required]
        public int idx { get; set; }

        /// <summary>
        /// 排队号码
        /// </summary>
        [StringLength(8)]
        public string codec { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [StringLength(32)]
        public string label { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(32)]
        public string phone { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Required]
        public int lv { get; set; }

        /// <summary>
        /// 呼叫次数
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public QcsQueueHandleEnums handle { get; set; }
    }
}