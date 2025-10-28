using Com.Scm.Quartz.Dao;

namespace Com.Scm.Quartz
{
    /// <summary>
    /// 任务管理服务
    /// </summary>
    public interface IQuartzService
    {
        Task<JobResult> AddJob(QuarzTaskDao taskOptions);

        Task<List<QuarzTaskDao>> GetJobs();

        void InitJobs();

        Task<JobResult> IsQuartzJob(string taskName, string groupName);

        JobResult IsValidExpression(string cronExpression);

        Task<JobResult> Pause(QuarzTaskDao taskOptions);

        Task<JobResult> Remove(QuarzTaskDao taskOptions);

        Task<JobResult> Run(QuarzTaskDao taskOptions);

        Task<JobResult> Start(QuarzTaskDao taskOptions);

        Task<JobResult> Update(QuarzTaskDao taskOptions);
    }
}