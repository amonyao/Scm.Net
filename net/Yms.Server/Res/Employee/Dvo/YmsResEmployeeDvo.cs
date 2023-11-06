using Com.Scm.Dvo;

namespace Com.Scm.Yms.Res.Employee.Dvo
{
    /// <summary>
    /// 租客
    /// </summary>
    public class YmsResEmployeeDvo : ScmDataDvo
    {
        /// <summary>
        /// 所属商户
        /// </summary>
        public long merchant_id { get; set; }
        public string merchant_names { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 租客编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string names { get; set; }

        /// <summary>
        /// 租客名称
        /// </summary>
        public string namec { get; set; }
    }
}