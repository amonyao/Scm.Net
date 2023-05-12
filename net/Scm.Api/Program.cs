using Com.Scm.Api.Filters;
using Com.Scm.Api.Hubs;
using Com.Scm.Api.Middleware;
using Com.Scm.Config;
using Com.Scm.Dsa.Dba.Sugar;
using Com.Scm.Email.Config;
using Com.Scm.Extensions;
using Com.Scm.Generator;
using Com.Scm.Generator.Config;
using Com.Scm.Helper;
using Com.Scm.Ioc;
using Com.Scm.Mapper;
using Com.Scm.Quartz.Config;
using Com.Scm.Quartz.Extensions;
using Com.Scm.Service;
using Com.Scm.Swagger;
using Com.Scm.Uid;
using Com.Scm.Uid.Config;
using Com.Scm.Utils;
using System.Text.Json.Serialization;

namespace Com.Scm.Api
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AppUtils.Configuration = builder.Configuration;

            // SignalR
            builder.Services.AddSignalR();

            // 环境变量
            var envConfig = AppUtils.GetConfig<EnvConfig>(EnvConfig.NAME) ?? new EnvConfig();
            envConfig.Prepare(builder);
            builder.Services.AddSingleton(envConfig);

            // 跨域访问
            builder.Services.CorsSetup();

            // 全局过滤
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<AopActionFilter>();
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<UnitOfWorkFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.AllowTrailingCommas = false;
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new LongJsonConverter());
            });

            // Swagger配置
            var swaggerConfig = AppUtils.GetConfig<SwaggerConfig>(SwaggerConfig.NAME);
            builder.Services.SwaggerSetup(swaggerConfig);

            // Sqlsugar配置
            builder.Services.SqlSugarSetup();

            // 缓存配置
            builder.Services.DsaCacheSetup();

            // UID配置
            var uidConfig = AppUtils.GetConfig<UidConfig>(UidConfig.NAME);
            UidHelper.InitConfig(uidConfig);

            var apiConfig = AppUtils.GetConfig<ApiConfig>(ApiConfig.NAME);
            apiConfig.Prepare(builder.Environment);
            builder.Services.RegisterServices(apiConfig);

            // 代码生成
            var genConfig = AppUtils.GetConfig<GeneratorConfig>(GeneratorConfig.NAME);
            builder.Services.GeneratorSetup(genConfig);

            // Quartz
            var quartzConfig = AppUtils.GetConfig<QuartzConfig>(QuartzConfig.NAME) ?? new QuartzConfig();
            builder.Services.AddQuartz(quartzConfig);
            builder.Services.AddQuartzClassJobs();

            // EMail
            var emailConfig = AppUtils.GetConfig<EmailConfig>(EmailConfig.NAME) ?? new EmailConfig();
            builder.Services.AddSingleton(emailConfig);

            builder.Services.AddScoped<IDicService, ScmDicService>();
            builder.Services.AddScoped<ICfgService, ScmCfgService>();
            builder.Services.AddScoped<IUserService, ScmUserService>();

            // Mapper
            builder.Services.AddMapperProfile();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerSetup(swaggerConfig);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "upload")),
            //    RequestPath = "/upload",
            //});

            app.UseSetup();

            app.MapControllers().RequireAuthorization();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}