using Com.Scm.Cms.Enums;
using Com.Scm.Dao.Unit;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Com.Scm.Gtd
{
    /// <summary>
    /// 待办
    /// </summary>
    [SugarTable("scm_gtd_header")]
    public class GtdHeaderDao : ScmUnitDataDao
    {
        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long cat_id { get; set; }

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
        /// 紧急性
        /// </summary>
        public int urgent { get; set; }

        /// <summary>
        /// 重要性
        /// </summary>
        public int important { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public GtdPriorityEnum priority { get; set; }

        /// <summary>
        /// 提醒标识
        /// </summary>
        public GtdRemindEnum remind { get; set; }

        /// <summary>
        /// 表达式
        /// </summary>
        [StringLength(128)]
        public string cron { get; set; }

        /// <summary>
        /// 提示方式
        /// </summary>
        public GtdNoticeEnum notice { get; set; }

        /// <summary>
        /// 上次提醒时间
        /// </summary>
        public long last_time { get; set; }

        /// <summary>
        /// 下次提醒时间
        /// </summary>
        public long next_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GtdHandleEnum handle { get; set; }
    }
}