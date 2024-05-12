using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 商品
    /// </summary>
    public class PosResSpuDto : ScmDataDto
    {
        /// <summary>
        /// 品类ID
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(64)]
        public string names { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }
    }
}