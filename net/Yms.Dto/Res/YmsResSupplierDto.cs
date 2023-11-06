using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Res
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class YmsResSupplierDto : ScmDataDto
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }
    }
}