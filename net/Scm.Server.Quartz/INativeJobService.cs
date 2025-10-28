namespace Com.Scm.Quartz
{
    /// <summary>
    /// 本地服务
    /// </summary>
    public interface INativeJobService
    {
        string ExecuteService(string parameter);
    }
}
