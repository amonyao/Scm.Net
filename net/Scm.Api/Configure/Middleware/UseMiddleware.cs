using Com.Scm.Quartz.Extensions;
using Com.Scm.Utils;

namespace Com.Scm.Api.Middleware;

public static class UseMiddleware
{
    public static void UseSetup(this IApplicationBuilder app)
    {
        app.UseRouting();

        // 跨域设置
        app.UseCors("ScmCors");

        // DI
        AppUtils.ServiceProvider = app.ApplicationServices;

        app.Use(next => async context =>
        {
            context.Request.EnableBuffering();
            await next(context);
        });
        // 认证
        app.UseAuthentication();
        // 授权
        app.UseAuthorization();
        // 中间件异常处理
        app.UseMiddleware<ExceptionMiddleware>();
        // Jwt中间件处理
        app.UseMiddleware<JwtMiddleware>();

        app.UseQuartz();

        /*app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ScmHub>("/scmhub");
        });*/
    }
}