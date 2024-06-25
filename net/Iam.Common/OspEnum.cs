namespace Com.Scm.OAuth.Web.Dto
{
    /// <summary>
    /// 排序依据
    /// </summary>
    public enum OspOrderByEnum
    {
        None,
        /// <summary>
        /// 默认
        /// </summary>
        Id,
        /// <summary>
        /// 指定顺序
        /// </summary>
        Od,
        /// <summary>
        /// 使用频次
        /// </summary>
        Qty
    }

    /// <summary>
    /// OAuth协议版本，目前有V1和V2版本
    /// </summary>
    public enum OspVerEnum
    {
        None,
        V1,
        V2,
    }

    public enum OspShowMoreEnum
    {
        None,
        /// <summary>
        /// 不显示
        /// </summary>
        Hide,
        /// <summary>
        /// 总是显示
        /// </summary>
        Always,
        /// <summary>
        /// 超出显示
        /// </summary>
        AsNeed
    }
}
