using Com.Scm.Api.Configure.Filters;
using Com.Scm.Api.Configure.Middleware;
using Com.Scm.Config;
using Com.Scm.Dsa.Dba.Sugar.UnitOfWork.Filters;
using Com.Scm.Email.Config;
using Com.Scm.Extensions;
using Com.Scm.Generator;
using Com.Scm.Generator.Config;
using Com.Scm.Hubs;
using Com.Scm.Mapper;
using Com.Scm.Quartz.Config;
using Com.Scm.Quartz.Extensions;
using Com.Scm.Server;
using Com.Scm.Service;
using Com.Scm.Uid.Config;
using Com.Scm.Utils;
using Microsoft.Extensions.FileProviders;

namespace Com.Scm.Api
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AppUtils.Init(builder.Configuration);

            // LOG����
            //Serilog.Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(builder.Configuration)
            //    .CreateLogger();
            Logger.Setup();

            var services = builder.Services;

            // ��������
            var envConfig = AppUtils.GetConfig<EnvConfig>(EnvConfig.NAME) ?? new EnvConfig();
            envConfig.Prepare(builder);
            services.AddSingleton(envConfig);

            // Sql����
            var sqlConfig = AppUtils.GetConfig<SqlConfig>(SqlConfig.NAME);
            sqlConfig.Prepare(envConfig);
            services.SqlSugarSetup(sqlConfig);

            // Uid����
            var uidConfig = AppUtils.GetConfig<UidConfig>(UidConfig.NAME);
            UidUtils.InitConfig(uidConfig);

            // ��������
            services.CacheSetup(envConfig);

            // Swagger����
            var swaggerConfig = AppUtils.GetConfig<SwaggerConfig>(SwaggerConfig.NAME);
            services.SwaggerSetup(swaggerConfig);

            // ��������
            var dataConfig = AppUtils.GetConfig<DataConfig>(DataConfig.NAME) ?? new DataConfig();
            dataConfig.Prepare(builder.Environment);
            services.AddSingleton(dataConfig);

            // ��ȫ����
            var secConfig = AppUtils.GetConfig<SecurityConfig>(SecurityConfig.NAME);
            secConfig.Prepare(builder.Environment);
            services.AddSingleton(secConfig);

            // ��������
            var genConfig = AppUtils.GetConfig<GeneratorConfig>(GeneratorConfig.NAME);
            genConfig.Prepare(envConfig);
            services.GeneratorSetup(genConfig);

            // Quartz
            var quartzConfig = AppUtils.GetConfig<QuartzConfig>(QuartzConfig.NAME) ?? new QuartzConfig();
            services.AddQuartz(quartzConfig);
            services.AddQuartzClassJobs();

            // EMail
            var emailConfig = AppUtils.GetConfig<EmailConfig>(EmailConfig.NAME) ?? new EmailConfig();
            services.AddSingleton(emailConfig);

            // Aiml
            var aimlConfig = AppUtils.GetConfig<AimlConfig>(AimlConfig.NAME) ?? new AimlConfig();
            aimlConfig.Prepare(envConfig);
            services.AddSingleton(aimlConfig);

            services.AddScoped<ILogService, ScmLogService>();
            services.AddScoped<IDicService, ScmDicService>();
            services.AddScoped<ICfgService, ScmCfgService>();
            services.AddScoped<ISecService, ScmSecService>();
            services.AddScoped<ICatService, ScmCatService>();
            services.AddScoped<ITagService, ScmTagService>();
            services.AddScoped<ISmsService, ScmSmsService>();

            // ȫ�ֹ���
            services.AddControllers(options =>
            {
                options.Filters.Add<AopActionFilter>();
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<UnitOfWorkFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).JsonSetup();

            // �ӿ�����
            var apiConfig = AppUtils.GetConfig<DllConfig>(DllConfig.NAME);
            apiConfig.Prepare(builder.Environment);
            services.RegisterServices(apiConfig);

            // Jwt Config
            services.SetupJwt();

            // �������
            services.CorsSetup();

            // SignalR
            services.AddSignalR();

            // Mapper
            services.AddMapperProfile();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerSetup(swaggerConfig);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!string.IsNullOrEmpty(envConfig.DataUri))
            {
                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(envConfig.DataDir),
                    RequestPath = envConfig.DataUri,
                });
            }

            app.UseSetup();

            app.MapControllers().RequireAuthorization();
            app.MapHub<ScmHub>("/scmhub");

            app.Run();
        }
    }
}