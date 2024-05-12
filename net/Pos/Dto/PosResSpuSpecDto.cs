using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 规格信息
    /// </summary>
    public class PosResSpuSpecDto : ScmDataDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public long spu_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 规格编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(64)]
        public string names { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        public int price { get; set; }
    }
}