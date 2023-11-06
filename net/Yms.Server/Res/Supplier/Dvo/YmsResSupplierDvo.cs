using Com.Scm.Dvo;

namespace Com.Scm.Yms.Res.Supplier.Dvo
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class YmsResSupplierDvo : ScmDataDvo
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string namec { get; set; }
    }
}