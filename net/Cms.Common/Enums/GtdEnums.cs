namespace Com.Scm.Cms.Enums
{
    /// <summary>
    /// 优先级
    /// </summary>
    public enum GtdPriorityEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 默认
        /// </summary>
        Normal = 1,

    }

    /// <summary>
    /// 提醒类型
    /// </summary>
    public enum GtdRemindEnum
    {
        None = 0,
    }

    /// <summary>
    /// 提示方式
    /// </summary>
    public enum GtdNoticeEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        Sms = 1,
        Email = 2,
        Alert = 3,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum GtdHandleEnum
    {
        None = 0,
        Todo = 1,
        Doing = 2,
        Done = 3,
    }
}
