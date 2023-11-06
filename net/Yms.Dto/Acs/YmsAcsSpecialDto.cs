using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Acs
{
    /// <summary>
    /// 黑白名单
    /// </summary>
    public class YmsAcsSpecialDto : ScmDataDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public AcsSpecialTypesEnums types { get; set; }

        /// <summary>
        /// 实体
        /// </summary>
        [Required]
        public AcsSpecialModesEnums modes { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        [StringLength(128)]
        public string driver { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [StringLength(128)]
        public string reason { get; set; }
    }
}