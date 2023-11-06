using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Fac
{
    /// <summary>
    /// 楼层
    /// </summary>
    public class YmsFacFloorDto : ScmDataDto
    {
        /// <summary>
        /// 所属园区
        /// </summary>
        [Required]
        public long area_id { get; set; }

        /// <summary>
        /// 所属楼宇
        /// </summary>
        [Required]
        public long build_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 楼层编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }
    }
}