using Com.Scm.Cms.Enums;
using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Cms.Gtd
{
    /// <summary>
    /// 待办（明细）
    /// </summary>
    [SugarTable("scm_gtd_detail")]
    public class GtdDetailDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long header_id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long time { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1024)]
        public string remark { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public GtdPriorityEnum priority { get; set; }

        /// <summary>
        /// 提醒标识
        /// </summary>
        public GtdRemindEnum remind { get; set; }

        /// <summary>
        /// 提示方式
        /// </summary>
        public GtdNoticeEnum notice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GtdHandleEnum handle { get; set; }
    }
}
