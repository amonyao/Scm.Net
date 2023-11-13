using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Qcs
{
    /// <summary>
    /// 队列
    /// </summary>
    [SugarTable("scm_qcs_detail")]
    public class ScmQcsDetailDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 队列编码
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 队列说明
        /// </summary>
        [StringLength(256)]
        public string remark { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int od { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// 当前号码
        /// </summary>
        public int current { get; set; }

        /// <summary>
        /// 号码前缀
        /// </summary>
        [StringLength(4)]
        public string prefix { get; set; }


    }
}