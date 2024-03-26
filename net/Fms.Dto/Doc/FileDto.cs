using Com.Scm.Dto;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 图片数据
    /// </summary>
    public class FileDto : ScmDataDto
    {
        /// <summary>
        /// 类别
        /// </summary>
        [Required]
        public long pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int types { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int modes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(256)]
        public string codes { get; set; }

        /// <summary>
        /// 短链编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [Required]
        [StringLength(256)]
        public string names { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        [StringLength(256)]
        public string namec { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [StringLength(16)]
        public string path { get; set; }

        /// <summary>
        /// 文件摘要
        /// </summary>
        [StringLength(256)]
        public string hash { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        [StringLength(32)]
        public string exts { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Required]
        public long size { get; set; }

        /// <summary>
        /// 引用数量
        /// </summary>
        [Required]
        public int refs { get; set; } = 0;

        /// <summary>
        /// 标签数量
        /// </summary>
        [Required]
        public int tags { get; set; } = 0;

        /// <summary>
        /// 收藏数量
        /// </summary>
        [Required]
        public int favs { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        /// <summary>
        /// 文件创建时间
        /// </summary>
        [Required]
        public long file_create_time { get; set; }

        /// <summary>
        /// 文件修改时间
        /// </summary>
        public long file_modify_time { get; set; }


    }
}