using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public class PosResSpuExtsDto : ScmDataDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 属性内容
        /// </summary>
        [StringLength(512)]
        public string json { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        public int price { get; set; }
    }
}