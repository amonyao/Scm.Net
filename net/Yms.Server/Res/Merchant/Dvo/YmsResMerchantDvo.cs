using Com.Scm.Dvo;

namespace Com.Scm.Yms.Res.Merchant.Dvo
{
    /// <summary>
    /// 商户
    /// </summary>
    public class YmsResMerchantDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 商户编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string namec { get; set; }
    }
}