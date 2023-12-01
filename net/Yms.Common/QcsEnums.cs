using System.ComponentModel;

namespace Com.Scm.Yms
{
    public enum QcsQueueHandleEnums
    {
        None = 0,
        /// <summary>
        /// 待叫号
        /// </summary>
        Todo = 1,
        /// <summary>
        /// 服务中
        /// </summary>
        Doing = 2,
        /// <summary>
        /// 已完成
        /// </summary>
        Done = 3,
    }

    public enum QcsDetailCycleEnums
    {
        None = 0,
        [Description("每时")]
        Hour = 1,
        [Description("每天")]
        Day = 2,
        [Description("每周")]
        Week = 6,
        [Description("每月")]
        Month = 3,
        [Description("每季")]
        Season = 5,
        [Description("每年")]
        Year = 4,
    }
}
