using Com.Scm.Dao.Unit;
using Com.Scm.Utils;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 规格信息
    /// </summary>
    [SugarTable("pos_res_spu_spec")]
    public class PosResSpuSpecDao : ScmUnitDataDao
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public long spu_id { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 规格编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(64)]
        public string names { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        [StringLength(128)]
        public string namec { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        public int price { get; set; } = 0;



        public override void PrepareCreate(long userId, long unitId = 0)
        {
            base.PrepareCreate(userId, unitId);

            if (string.IsNullOrWhiteSpace(names))
            {
                names = namec;
            }
            codes = UidUtils.NextCodes("pos_res_spu_spec");
        }
    }
}