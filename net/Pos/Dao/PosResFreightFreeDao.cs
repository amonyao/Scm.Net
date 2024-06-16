using Com.Scm.Dao;
using Com.Scm.Pos.Enums;
using SqlSugar;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 包邮方案
    /// </summary>
    [SugarTable("pos_res_freight_free")]
    public class PosResFreightFreeDao : ScmDataDao
    {
        /// <summary>
        /// 运费方案
        /// </summary>
        public long freight_id { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public long region_id { get; set; }

        /// <summary>
        /// 计价单位
        /// </summary>
        public FreightUomEnums uom { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public int price { get; set; }
    }
}