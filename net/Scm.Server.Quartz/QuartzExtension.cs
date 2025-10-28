using Com.Scm.Quartz.Config;
using Com.Scm.Quartz.Jobs;
using Com.Scm.Quartz.Service;
using Com.Scm.Quartz.Service.Db;
using Com.Scm.Quartz.Service.Df;
using Com.Scm.Quartz.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Reflection;

namespace Com.Scm.Quartz
{
    public static class QuartzExtension
    {
        public static IServiceCollection AddQuartz(this IServiceCollection services, QuartzConfig config)
        {
            if (config == null)
            {
                return services;
            }

            config.LoadDef();

            //services.AddRazorPages();
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            if (string.IsNullOrWhiteSpace(config.Type) || config.Type == "file")
            {
                services.SetQuarzFileContext(config);
                //services.AddScoped<QuartzFileHelper>();
                services.AddScoped<IQuartzLogService, DfQuartzLogService>();
                services.AddScoped<IQuartzService, DfQuartzJobService>();
            }
            else
            {
                services.SetQuarzSugarContext(config);
                services.AddScoped<IQuartzLogService, DbQuartzLogService>();
                services.AddScoped<IQuartzService, DbQuartzJobService>();
            }

            services.AddScoped<HttpResultfulJob>();
            services.AddScoped<ClassLibraryJob>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddScoped<IQuartzJobService, Service.Job.QuartzJobService>();
            return services;
        }

        /// <summary>
        /// 自动注入定时任务类
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQuartzClassJobs(this IServiceCollection services)
        {
            var baseType = typeof(INativeJobService);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = Directory.GetFiles(path, "*.dll");
            List<Type> typelist = new List<Type>();
            foreach (var item in referencedAssemblies)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(item);
                    Type[] ts = assembly.GetTypes();
                    typelist.AddRange(ts.ToList());
                }
                catch (Exception)
                {
                    continue;
                }
            }

            var types = typelist.Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            var implementTypes = types.Where(x => x.IsClass).ToArray();
            foreach (var implementType in implementTypes)
            {
                var interfaceType = implementType.GetInterfaces().First();
                services.AddScoped(interfaceType, implementType);
            }

            //var interfaceTypes = types.Where(x => x.IsInterface).ToArray();

            return services;
        }

        public static IApplicationBuilder UseQuartz(this IApplicationBuilder builder)
        {
            var services = builder.ApplicationServices;
            using var serviceScope = services.CreateScope();
            var handle = serviceScope.ServiceProvider.GetService<IQuartzJobService>();
            handle?.InitJobs();

            return builder;
        }
    }
}
