using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 扩展属性
    /// </summary>
    public class PosSpuExtsDto : ScmDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string value { get; set; }
    }
}