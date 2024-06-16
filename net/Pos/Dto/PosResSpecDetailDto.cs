using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 规格明细
    /// </summary>
    public class PosResSpecDetailDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; } 

        /// <summary>
        /// 规格编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }
    }
}