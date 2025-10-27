using Com.Scm.Quartz.Dao;
using SqlSugar;
using System.Linq.Expressions;

namespace Com.Scm.Quartz.Service.Db
{
    public class DbQuartzJobService : IQuartzService
    {
        private ISqlSugarClient _Client;

        public DbQuartzJobService(ISqlSugarClient client)
        {
            _Client = client;
        }

        public async Task<JobResult> AddJob(QuarzTaskDao model)
        {
            var date = await _Client.Insertable(model).ExecuteCommandAsync();
            var result = new JobResult { status = false, message = "" };
            if (date <= 0) return result;
            result.status = true;
            result.message = "数据库添加成功!";
            return result;
        }

        public async Task<List<QuarzTaskDao>> GetJobs(Expression<Func<QuarzTaskDao, bool>> where = null)
        {
            return await _Client.Queryable<QuarzTaskDao>().Where(where).ToListAsync();
        }

        public async Task<JobResult> Remove(QuarzTaskDao model)
        {
            var date = await _Client.Deleteable<QuarzTaskDao>().Where(m => m.id == model.id).ExecuteCommandAsync();
            var result = new JobResult { status = false, message = "" };
            if (date <= 0) return result;
            result.status = true;
            result.message = "数据库删除成功!";
            return result;
        }

        public async Task<JobResult> Update(QuarzTaskDao model)
        {
            var date = await _Client.Updateable(model).ExecuteCommandAsync();
            var result = new JobResult { status = false, message = "" };
            if (date <= 0) return result;
            result.status = true;
            result.message = "数据库修改成功!";
            return result;
        }
    }
}
