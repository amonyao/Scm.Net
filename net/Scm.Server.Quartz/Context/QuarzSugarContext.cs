using Com.Scm.Quartz.Config;
using Com.Scm.Quartz.Dao;
using Com.Scm.Quartz.Service.Df;
using Com.Scm.Utils;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Com.Scm.Quartz
{
    public static class QuarzSugarContext
    {
        public static void SetQuarzSugarContext(this IServiceCollection services, QuartzConfig config)
        {
            var sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = SqlSugarUtils.GetDbType(config.Type),
                ConnectionString = config.ConnectionString,
                IsAutoCloseConnection = true
            });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);

            ModelCreating(sqlSugar);
        }

        private static void ModelCreating(ISqlSugarClient db)
        {
            db.CodeFirst.InitTables(typeof(QuarzTaskDao));
            db.CodeFirst.InitTables(typeof(QuarzTaskLogDao));
        }

        public static void SetQuarzFileContext(this IServiceCollection services, QuartzConfig config)
        {
            var fileHelper = new QuartzFileHelper(config);
            services.AddSingleton(fileHelper);
        }
    }
}
