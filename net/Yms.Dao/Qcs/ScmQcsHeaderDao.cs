using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Yms.Qcs
{
    /// <summary>
    /// 方案
    /// </summary>
    [SugarTable("scm_qcs_header")]
    public class ScmQcsHeaderDao : ScmUnitDataDao
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        [StringLength(16)]
        public string codes { get; set; }

        /// <summary>
        /// 方案编码
        /// </summary>
        [StringLength(32)]
        public string codec { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [StringLength(16)]
        public string names { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        [StringLength(32)]
        public string namec { get; set; }

        /// <summary>
        /// 上级方案
        /// </summary>
        public long pid { get; set; }

        /// <summary>
        /// 队列数量
        /// </summary>
        public int qty { get; set; }
    }
}