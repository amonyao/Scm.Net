using Com.Scm.Dao.Unit;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 商品
    /// </summary>
    [SugarTable("pos_res_spu")]
    public class PosResSpuDao : ScmUnitDataDao
    {
        /// <summary>
        /// 品类ID
        /// </summary>
        [Required]
        public long cat_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(64)]
        public string names { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            if (string.IsNullOrWhiteSpace(names))
            {
                names = namec;
            }
            codes = UidUtils.NextCodes("pos_res_spu");
        }
    }
}