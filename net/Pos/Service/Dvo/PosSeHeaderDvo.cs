using Com.Scm.Dvo;
using Com.Scm.Pos.Enums;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 促销头档
    /// </summary>
    public class PosSeHeaderDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 活动编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string namec { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public SeTypesEnums types { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public long f_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
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
        public int qty { get; set; }
    }
}