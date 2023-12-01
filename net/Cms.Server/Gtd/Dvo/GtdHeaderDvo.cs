using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Gtd.Dvo
{
    /// <summary>
    /// 待办
    /// </summary>
    public class GtdHeaderDvo : ScmDataDvo
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
        public string title { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
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
        /// 表达式
        /// </summary>
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