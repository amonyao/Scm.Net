using Com.Scm.Dvo;
using Com.Scm.Pos.Enums;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 订单头档
    /// </summary>
    public class PosSoOrderHeaderDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 
        /// </summary>
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