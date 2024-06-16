using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 商品图片
    /// </summary>
    public class PosSpuImageDto : ScmDataDto
    {
        /// <summary>
        /// 商品
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [StringLength(256)]
        public string image { get; set; }

        /// <summary>
        /// 图片说明
        /// </summary>
        [StringLength(128)]
        public string remark { get; set; }
    }
}