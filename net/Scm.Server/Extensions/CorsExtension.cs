using Com.Scm.Config;
using Com.Scm.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Scm.Server
{
    public static class CorsExtension
    {
        public static void CorsSetup(this IServiceCollection services)
        {
            var config = AppUtils.GetConfig<CorConfig>(CorConfig.NAME);
            if (config == null || config.Origion == null)
            {
                return;
            }

            services.AddCors(options =>
            {
                options.AddPolicy(name: "ScmCors",
                    policy =>
                    {
                        policy.WithOrigins(config.Origion)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .WithExposedHeaders("X-Refresh-Token");
                    });
            });
        }
    }
}
