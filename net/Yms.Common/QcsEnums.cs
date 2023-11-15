using System.ComponentModel;

namespace Com.Scm.Yms
{
    public enum QcsQueueHandleEnums
    {
        None = 0,
        Todo = 1,
        Doing = 2,
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
