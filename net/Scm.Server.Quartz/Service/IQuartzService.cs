using Com.Scm.Quartz.Dao;
using System.Linq.Expressions;

namespace Com.Scm.Quartz.Service
{
    public interface IQuartzService
    {
        /// <summary>
        /// 获取所有作业
        /// </summary>
        /// <returns></returns>
        Task<List<QuarzTaskDao>> GetJobs(Expression<Func<QuarzTaskDao, bool>> where = null);

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<JobResult> AddJob(QuarzTaskDao model);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<JobResult> Remove(QuarzTaskDao model);

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        Task<JobResult> Update(QuarzTaskDao model);
    }
}
