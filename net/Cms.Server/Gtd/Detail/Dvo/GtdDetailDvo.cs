using Com.Scm.Cms.Enums;
using Com.Scm.Dvo;

namespace Com.Scm.Cms.Gtd.Detail.Dvo
{
    public class GtdDetailDvo : ScmDataDvo
    {
        /// <summary>
        /// 
        /// </summary>
        public long time { get; set; }

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
        /// 提示方式
        /// </summary>
        public GtdNoticeEnum notice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GtdHandleEnum handle { get; set; }
    }
}
