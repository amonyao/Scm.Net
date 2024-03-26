using Com.Scm.Dao;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Fms.Res
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("fes_res_cat")]
    public class CatDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(32)]
        public string names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(1024)]
        public string path { get; set; }

        /// <summary>
        /// 上级类别
        /// </summary>
        [Required]
        public long pid { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        [Required]
        public int od { get; set; } = 0;

        /// <summary>
        /// 图像数量
        /// </summary>
        [Required]
        public int qty { get; set; } = 0;

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            codes = UidUtils.NextCodes("fes_res_cat");
        }
    }
}