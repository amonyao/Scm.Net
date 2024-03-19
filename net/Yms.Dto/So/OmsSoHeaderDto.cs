using Com.Scm.Dto;
using Com.Scm.Utils;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.So
{
    /// <summary>
    /// 订单主档
    /// </summary>
    public class OmsSoHeaderDto : ScmDataDto
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        [StringLength(32)]
        public string codes { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [StringLength(64)]
        public string codec { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        [Required]
        public int sale_amt { get; set; }
        public string sale_amt_text { get { return TextUtils.FormatPrice(sale_amt); } set { sale_amt = TextUtils.ParsePrice(value); } }

        /// <summary>
        /// 成交金额
        /// </summary>
        [Required]
        public int paid_amt { get; set; }
        public string paid_amt_text { get { return TextUtils.FormatPrice(paid_amt); } set { paid_amt = TextUtils.ParsePrice(value); } }

        /// <summary>
        /// 订单备注
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }
    }
}