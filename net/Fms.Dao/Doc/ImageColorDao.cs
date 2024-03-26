using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Doc
{
    /// <summary>
    /// 图片颜色
    /// </summary>
    [SugarTable("fes_doc_image_color")]
    public class ImageColorDao : ScmDataDao
    {
        /// <summary>
        /// 图像
        /// </summary>
        [Required]
        public long file_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 颜色
        /// </summary>
        [Required]
        public long color_id { get; set; }
    }
}