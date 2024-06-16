using Com.Scm.Dto;
using Com.Scm.Pos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 运费
    /// </summary>
    public class PosResFreightDto : ScmDataDto
    {
        /// <summary>
        /// 运费编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 运费名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 计费方案
        /// </summary>
        [Required]
        public FreightCostTypesEnums cost_types { get; set; }

        /// <summary>
        /// 包邮方案
        /// </summary>
        [Required]
        public FreightFreeTypesEnums free_types { get; set; }
    }
}