using Com.Scm.Cms.Enums;
using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    public class CmsBlogDto : ScmDataDto
    {
        public const long SYS_ID = 1000000000000000001;
        public const int SUMMARY_SIZE = 1024;

        /// <summary>
        /// 键
        /// </summary>
        [StringLength(32)]
        public string key { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public BlogTypesEnum types { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string sub_title { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int qty0 { get; set; }

        /// <summary>
        /// 收藏数量
        /// </summary>
        [Required]
        public int fav_qty { get; set; }

        /// <summary>
        /// 留言数量
        /// </summary>
        [Required]
        public int msg_qty { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required]
        public int files { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required, StringLength(1024)]
        public string summary { get; set; }
    }
}