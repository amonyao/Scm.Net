using Com.Scm.Dao;
using Com.Scm.Pos.Enums;
using SqlSugar;

namespace Com.Scm.Pos.Res
{
    /// <summary>
    /// 计费方案
    /// </summary>
    [SugarTable("pos_res_freight_cost")]
    public class PosResFreightCostDao : ScmDataDao
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
        /// 起步数量
        /// </summary>
        public int min_qty { get; set; }

        /// <summary>
        /// 起步价格
        /// </summary>
        public int min_price { get; set; }

        /// <summary>
        /// 步进数量
        /// </summary>
        public int step_qty { get; set; }

        /// <summary>
        /// 步进价格
        /// </summary>
        public int step_price { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int max_qty { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public int max_price { get; set; }
    }
}