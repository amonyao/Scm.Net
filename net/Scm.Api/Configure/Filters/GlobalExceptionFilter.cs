using Com.Scm.Enums;
using Com.Scm.Exceptions;
using Com.Scm.Log;
using Com.Scm.Log.Api;
using Com.Scm.Operator;
using Com.Scm.Result;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Com.Scm.Api.Filters;

/// <summary>
/// 全局异常处理
/// </summary>
public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    readonly IWebHostEnvironment _hostEnvironment;
    private LogApiService _logService;
    private OperatorService _operatorService;

    public GlobalExceptionFilter(IWebHostEnvironment hostEnvironment
        , LogApiService logService
        , OperatorService operatorService)
    {
        _hostEnvironment = hostEnvironment;
        _logService = logService;
        _operatorService = operatorService;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.ExceptionHandled) return;

        #region 保存异常日志
        var user = _operatorService.GetTokenUser();
        var type = (context.ActionDescriptor as ControllerActionDescriptor)?.ControllerTypeInfo.AsType();
        if (type != null)
        {
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            var logInfo = new LogApiDto()
            {
                Level = LogEnum.Error,
                types = LogTypeEnum.Operate,
                module = type.FullName,
                method = context.HttpContext.Request.Method,
                operate_user = user.Username,
                ip = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString,
                browser = ServerUtils.GetBrowser(userAgent),
                agent = userAgent,
                message = context.Exception.Message,
                operate_time = DateTime.Now
            };
            //保存日志信息
            await _logService.AddAsync(logInfo);
        }
        #endregion

        var result = new ApiResult<string>();
        if (context.Exception is BusinessException e)
        {
            result.Code = (int)HttpStatusCode.Found;
            result.Message = e.GetMessage();
        }
        else
        {
            result.Code = (int)HttpStatusCode.InternalServerError;
            result.Message = context.Exception.Message;
        }

        context.Result = new JsonResult(result);
        context.ExceptionHandled = true;
    }
}