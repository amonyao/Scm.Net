using Com.Scm.Dto;
using Com.Scm.Pos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 促销头档
    /// </summary>
    public class PosSeHeaderDto : ScmDataDto
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 活动编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(64)]
        public string names { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        [Required]
        public SeTypesEnums types { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        [Required]
        public long f_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public long t_time { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 规格ID
        /// </summary>
        public long spec_id { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int qty { get; set; }
    }
}