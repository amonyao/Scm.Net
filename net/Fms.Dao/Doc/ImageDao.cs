using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 图片数据
    /// </summary>
    [SugarTable("fes_doc_image")]
    public class ImageDao : ScmUnitDataDao
    {
        /// <summary>
        /// 宽
        /// </summary>
        [Required]
        public int width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        [Required]
        public int height { get; set; }

        /// <summary>
        /// EXIF信息
        /// </summary>
        [StringLength(1024)]
        public string exif { get; set; }
    }
}