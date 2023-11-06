using Com.Scm.Dvo;

namespace Com.Scm.Yms.Acs.Special.Dvo
{
    /// <summary>
    /// 黑白名单
    /// </summary>
    public class YmsAcsSpecialDvo : ScmDataDvo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public AcsSpecialTypesEnums types { get; set; }

        /// <summary>
        /// 实体（人员、车辆）
        /// </summary>
        public AcsSpecialModesEnums modes { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        public string driver { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string reason { get; set; }
    }
}