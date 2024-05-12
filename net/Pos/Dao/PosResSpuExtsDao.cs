using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    [SugarTable("pos_res_spu_exts")]
    public class PosResSpuExtsDao : ScmUnitDataDao
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 属性内容
        /// </summary>
        [StringLength(512)]
        public string json { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        public int price { get; set; }
    }
}