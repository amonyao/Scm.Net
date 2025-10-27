using Com.Scm.Quartz.Dao;

namespace Com.Scm.Quartz.Service
{
    public interface IQuartzLogService
    {
        Task<ResultData<QuarzTaskLogDao>> GetLogs(string taskName, string groupName, int page, int pageSize = 100);

        Task<QuarzTaskLogDao> GetLastlog(string taskName, string groupName);

        Task<JobResult> AddLog(QuarzTaskLogDao log);
    }
}
