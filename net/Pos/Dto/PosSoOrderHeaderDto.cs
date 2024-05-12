using Com.Scm.Dto;
using Com.Scm.Pos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 订单头档
    /// </summary>
    public class PosSoOrderHeaderDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int item_need_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int item_real_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int unit_need_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int unit_real_qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SoHandleEnums handle { get; set; }
    }
}