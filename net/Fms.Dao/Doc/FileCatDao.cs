using Com.Scm.Dao.Unit;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Doc
{
    /// <summary>
    /// 图像软链
    /// </summary>
    [SugarTable("fes_doc_file_cat")]
    public class FileCatDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(32)]
        public string codes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(256)]
        public string namec { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 图像
        /// </summary>
        [Required]
        public long file_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public char salt { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            codes = UidUtils.NextCodes("fes_doc_file_cat");
        }
    }
}