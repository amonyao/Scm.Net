using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Qcs
{
    /// <summary>
    /// 号码
    /// </summary>
    public class ScmQcsQueueDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 
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
        public string contact { get; set; }

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
        public int level { get; set; }

        /// <summary>
        /// 呼叫次数
        /// </summary>
        [Required]
        public int calling { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public int handle { get; set; }


    }
}