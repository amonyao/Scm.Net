using Com.Scm.Dao;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 规格明细
    /// </summary>
    [SugarTable("pos_res_spec_detail")]
    public class PosResSpecDetailDao : ScmDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 规格编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        [StringLength(32)]
        public string image { get; set; }
    }
}