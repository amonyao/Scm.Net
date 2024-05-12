using Com.Scm.Api.Configure.Filters;
using Com.Scm.Api.Configure.Middleware;
using Com.Scm.Config;
using Com.Scm.Dsa.Dba.Sugar.UnitOfWork.Filters;
using Com.Scm.Email.Config;
using Com.Scm.Generator;
using Com.Scm.Generator.Config;
using Com.Scm.Hubs;
using Com.Scm.Mapper;
using Com.Scm.Quartz.Config;
using Com.Scm.Quartz.Extensions;
using Com.Scm.Server;
using Com.Scm.Service;
using Com.Scm.Share;
using Com.Scm.Swagger;
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

            // SignalR
            builder.Services.AddSignalR();

            // ��������
            var envConfig = AppUtils.GetConfig<EnvConfig>(EnvConfig.NAME) ?? new EnvConfig();
            envConfig.Prepare(builder);
            builder.Services.AddSingleton(envConfig);

            // ��������
            var dataConfig = AppUtils.GetConfig<DataConfig>(DataConfig.NAME) ?? new DataConfig();
            dataConfig.Prepare(builder.Environment);
            builder.Services.AddSingleton(dataConfig);

            // �������
            builder.Services.CorsSetup();

            // ȫ�ֹ���
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<AopActionFilter>();
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<UnitOfWorkFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).JsonSetup();

            // Swagger����
            var swaggerConfig = AppUtils.GetConfig<SwaggerConfig>(SwaggerConfig.NAME);
            builder.Services.SwaggerSetup(swaggerConfig);

            // Sqlsugar����
            var sqlConfig = AppUtils.GetConfig<SqlConfig>(SqlConfig.NAME);
            sqlConfig.Prepare(envConfig);
            builder.Services.SqlSugarSetup(sqlConfig);

            // ��������
            builder.Services.CacheSetup(envConfig);

            // ��ȫ����
            var secConfig = AppUtils.GetConfig<SecurityConfig>(SecurityConfig.NAME);
            secConfig.Prepare(builder.Environment);
            builder.Services.AddSingleton(secConfig);

            // UID����
            var uidConfig = AppUtils.GetConfig<UidConfig>(UidConfig.NAME);
            UidUtils.InitConfig(uidConfig);

            // �ӿ�����
            var apiConfig = AppUtils.GetConfig<ApiConfig>(ApiConfig.NAME);
            apiConfig.Prepare(builder.Environment);
            builder.Services.RegisterServices(apiConfig);

            // ��������
            var genConfig = AppUtils.GetConfig<GeneratorConfig>(GeneratorConfig.NAME);
            genConfig.Prepare(envConfig);
            builder.Services.GeneratorSetup(genConfig);

            // Quartz
            var quartzConfig = AppUtils.GetConfig<QuartzConfig>(QuartzConfig.NAME) ?? new QuartzConfig();
            builder.Services.AddQuartz(quartzConfig);
            builder.Services.AddQuartzClassJobs();

            // EMail
            var emailConfig = AppUtils.GetConfig<EmailConfig>(EmailConfig.NAME) ?? new EmailConfig();
            builder.Services.AddSingleton(emailConfig);

            // Aiml
            var aimlConfig = AppUtils.GetConfig<AimlConfig>(AimlConfig.NAME) ?? new AimlConfig();
            aimlConfig.Prepare(envConfig);
            builder.Services.AddSingleton(aimlConfig);

            builder.Services.AddScoped<ILogService, ScmLogService>();
            builder.Services.AddScoped<IDicService, ScmDicService>();
            builder.Services.AddScoped<ICfgService, ScmCfgService>();
            builder.Services.AddScoped<ISecService, ScmSecService>();
            builder.Services.AddScoped<ICatService, ScmCatService>();
            builder.Services.AddScoped<ITagService, ScmTagService>();
            builder.Services.AddScoped<IUserService, ScmUserService>();
            builder.Services.AddScoped<IShareService, ShareService>();

            // Mapper
            builder.Services.AddMapperProfile();

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