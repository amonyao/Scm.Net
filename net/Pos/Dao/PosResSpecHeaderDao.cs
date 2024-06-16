using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 规格主档
    /// </summary>
    [SugarTable("pos_res_spec_header")]
    public class PosResSpecHeaderDao : ScmUnitDataDao
    {
        /// <summary>
        /// 分类
        /// </summary>
        public long cat_id { get; set; }

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
        /// 是否支持图片
        /// </summary>
        public int image { get; set; }

        //public override void PrepareCreate(long userId, long unitId = 0)
        //{
        //    base.PrepareCreate(userId, unitId);

        //    if (string.IsNullOrWhiteSpace(names))
        //    {
        //        names = namef;
        //    }
        //    codes = UidHelper.NextCodes("pos_res_spec_header");
        //}
    }
}