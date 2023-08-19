using Com.Scm.Config;
using Com.Scm.Crypto;
using Com.Scm.Enums;
using Com.Scm.Filter;
using Com.Scm.Jwt;
using Com.Scm.Log;
using Com.Scm.Log.Api;
using Com.Scm.Operator;
using Com.Scm.Result;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text;

namespace Com.Scm.Api.Filters;

/// <summary>
/// 方法过滤器
/// </summary>
public class AopActionFilter : IAsyncActionFilter
{
    private static readonly List<string> IgnoreApi = new()
    {
        "api/sysfile/",
        "api/captcha",
        "/chathub",
        "login"
    };

    private readonly SecurityConfig _Config;
    private readonly LogApiService _logService;
    private readonly OperatorService _operatorService;
    private readonly JwtContextHolder _jwtContextHolder;

    public AopActionFilter(SecurityConfig config, LogApiService logService, OperatorService operatorService, JwtContextHolder jwtContextHolder)
    {
        _Config = config;
        _logService = logService;
        _operatorService = operatorService;
        _jwtContextHolder = jwtContextHolder;
    }

    private static bool IsIgnoreApi(string url)
    {
        var isIgnore = false;
        foreach (var item in IgnoreApi)
        {
            if (url.Contains(item))
            {
                isIgnore = true;
                break;
            }
        }
        return isIgnore;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = _jwtContextHolder.GetToken();

        #region 判断授权Api资源
        /*var superRole = AppUtils.Configuration[KeyUtils.SUPERROLEID];
        var urls = context.HttpContext.Request.Path.ToString().ToLower();
        if (user.Id!=0 && !user.RoleArray.StrToListLong().Contains(long.Parse(superRole)) && context.HttpContext.Request.Method != "GET"
                                                                            && context.HttpContext.Request.Method != "OPTIONS"
                                                                            && !IsIgnoreApi(urls))
        {
            Console.WriteLine("=======判断权限========");
            var redisStr = RedisService.cli.Get(KeyUtils.AUTHORIZZATIONAPI + ":" + user.Id);
            var apiList = !string.IsNullOrEmpty(redisStr) ? JsonSerializer.Deserialize<List<SysMenuApiUrl>>(redisStr) : null;
            if (apiList != null && !apiList.Exists(api => api.method == context.HttpContext.Request.Method && urls.Contains(api.url.ToLower())))
            {
                context.Result = new JsonResult(JResult<int>.Error("您无权限访问当前资源"));
                return;
            }
        }*/
        #endregion

        #region 安全签名认证
        if (_Config.CheckSignature)
        {
            var request = context.HttpContext.Request;
            var urlPath = request.Path.ToString().ToLower();

            var isIgnore = IsIgnoreApi(urlPath) || SkipAudit(context);
            if (!isIgnore)
            {
                var method = request.Method;

                //客户的唯一标示
                var appkey = request.Headers["appkey"];

                //13位时间戳
                var timestamp = request.Headers["timestamp"];

                //签名
                var signature = request.Headers["signature"];

                if (string.IsNullOrEmpty(appkey) || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(signature))
                {
                    Logger.Info("ApiSecurity——请求不合法");
                    context.Result = new JsonResult(JResult<int>.Error("请求不合法"));
                    return;
                }

                if (!TextUtils.IsNumberic(timestamp, 10, 13))
                {
                    context.Result = new JsonResult(JResult<int>.Error("无效的时间戳"));
                    return;
                }

                if (appkey != _Config.AppKey)
                {
                    Logger.Info("ApiSecurity——请求不合法-k");
                    context.Result = new JsonResult(JResult<int>.Error("请求不合法-k"));
                    return;
                }

                //判断timespan是否有效
                var ts1 = long.Parse(timestamp);
                var ts2 = TimeUtils.GetUnixTime();
                bool falg = (ts2 - ts1) > 1200 * 1000; //1分钟有效
                if (falg)
                {
                    Logger.Info("ApiSecurity——请求不合法-t");
                    context.Result = new JsonResult(JResult<int>.Error("请求不合法-t"));
                    return;
                }
                //根据请求类型拼接参数

                IDictionary<string, string> parameters = new Dictionary<string, string>();
                string data = string.Empty;
                switch (method)
                {
                    case "POST":
                        context.HttpContext.Request.Body.Position = 0;
                        StreamReader stream = new StreamReader(context.HttpContext.Request.Body);
                        string body = await stream.ReadToEndAsync();
                        //Console.WriteLine("body："+ body);
                        data = body;
                        context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                        break;
                    case "PUT":
                        context.HttpContext.Request.Body.Position = 0;
                        StreamReader streamPut = new StreamReader(context.HttpContext.Request.Body);
                        string bodyPut = await streamPut.ReadToEndAsync();
                        //Console.WriteLine("put："+ bodyPut);
                        data = bodyPut;
                        context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                        break;
                    case "DELETE":
                        context.HttpContext.Request.Body.Position = 0;
                        StreamReader streamDel = new StreamReader(context.HttpContext.Request.Body);
                        string bodyDel = await streamDel.ReadToEndAsync();
                        //Console.WriteLine("put："+ bodyPut);
                        data = bodyDel;
                        context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                        break;
                    case "GET":
                        {
                            var query = request.Query;
                            foreach (var item in query)
                            {
                                parameters.Add(item.Key, item.Value);
                            }

                            // 第二步：把字典按Key的字母顺序排序
                            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
                            using IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

                            // 第三步：把所有参数名和参数值串在一起
                            StringBuilder stringBuilder = new StringBuilder();
                            while (dem.MoveNext())
                            {
                                string key = dem.Current.Key;
                                string value = dem.Current.Value;
                                if (!string.IsNullOrEmpty(key))
                                {
                                    stringBuilder.Append(key).Append(value);
                                }
                            }

                            data = stringBuilder.ToString();
                            //Console.WriteLine("GET："+JsonConvert.SerializeObject(data));
                            break;
                        }
                }

                if (!ApiSecurityValidate(timestamp, appkey, data, signature))
                {
                    Logger.Info("ApiSecurity——参数不合法-Sign");
                    context.Result = new JsonResult(JResult<int>.Error("参数不合法"));
                    return;
                }
                //Console.WriteLine("success");
            }
        }
        #endregion

        //验证实体
        if (!context.ModelState.IsValid)
        {
            var message = string.Join(" | ", context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            Console.WriteLine("错误内容：" + message);
            context.Result = new JsonResult(JResult<string>.Error("参数不能为空~"));
            return;
        }

        //开始计时
        var stopwatch = Stopwatch.StartNew();
        var actionResult = await next();
        stopwatch.Stop();
        //读取返回类型以及数据
        var (isObject, actionData, logResult) = CheckResult(actionResult.Result);

        #region 收集日志信息
        if (!SkipLogging(context))
        {
            //接口Type
            var type = (context.ActionDescriptor as ControllerActionDescriptor)?.ControllerTypeInfo.AsType();
            var arguments = context.ActionArguments;
            var parametersStr = string.Empty;
            if (arguments.Count > 0)
            {
                parametersStr = TextUtils.ToJsonString(arguments);
            }

            var userAgent = ServerUtils.GetUserAgent();
            var now = DateTime.Now;
            //构建实体
            var logInfo = new LogApiDto()
            {
                level = LogLevelEnum.Info,
                types = LogTypesEnum.Operate,
                module = type?.FullName,
                method = context.HttpContext.Request.Method,
                operate_user = user.user_name,
                operate_date = TimeUtils.FormatDate(now),
                operate_time = TimeUtils.GetUnixTime(now),
                parameters = parametersStr,
                ip = ServerUtils.GetIp(),
                url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString,
                browser = ServerUtils.GetBrowser(userAgent),
                agent = userAgent
            };
            logInfo.duration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
            if (!string.IsNullOrEmpty(logResult))
            {
                logInfo.content = logResult.Replace("\\", "").Left(1000);
            }
            //保存日志信息
            await _logService.AddAsync(logInfo);
        }
        #endregion

        //返回统一格式
        if (isObject && !SkipJsonResult(context))
        {
            actionResult.Result = new JsonResult(JResult<object>.Success(actionData));
        }
        Console.WriteLine("Aop-Success");
    }

    /// <summary>
    /// 判断类和方法头上的特性是否要进行Action拦截
    /// </summary>
    /// <param name="actionContext"></param>
    /// <returns></returns>
    private static bool SkipAudit(ActionContext actionContext)
    {
        return actionContext.ActionDescriptor.EndpointMetadata.Any(m => m.GetType().FullName == typeof(AllowAnonymousAttribute).FullName);
    }

    /// <summary>
    /// 判断类和方法头上的特性是否要进行Action拦截
    /// </summary>
    /// <param name="actionContext"></param>
    /// <returns></returns>
    private static bool SkipLogging(ActionContext actionContext)
    {
        return actionContext.ActionDescriptor.EndpointMetadata.Any(m => m.GetType().FullName == typeof(NoAuditLogAttribute).FullName);
    }

    /// <summary>
    /// 判断类和方法头上的特性是否要进行非统一结果返回拦截
    /// </summary>
    /// <param name="actionContext"></param>
    /// <returns></returns>
    private static bool SkipJsonResult(ActionContext actionContext)
    {
        return actionContext.ActionDescriptor.EndpointMetadata.Any(m => m.GetType().FullName == typeof(NoJsonResultAttribute).FullName);
    }

    /// <summary>
    /// 验证参数是否正确
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <param name="appId"></param>
    /// <param name="data"></param>
    /// <param name="signature"></param>
    /// <returns></returns>
    private bool ApiSecurityValidate(string timeStamp, string appId, string data, string signature)
    {
        //签名key
        var signKey = _Config.SignKey;
        //拼接签名数据
        var signStr = appId + signKey + timeStamp + data;
        var newSign = CryptoUtils.Md5(signStr);
        return newSign == signature;
    }

    /// <summary>
    /// 验证返回类型是否满足Object格式
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static (bool, object, string) CheckResult(IActionResult result)
    {
        return result switch
        {
            ObjectResult objectResult => (true, objectResult.Value, TextUtils.ToJsonString(objectResult.Value)),
            JsonResult jsonResult => (true, jsonResult.Value, TextUtils.ToJsonString(jsonResult.Value)),
            ContentResult contentResult => (false, contentResult.Content, TextUtils.ToJsonString(contentResult.Content)),
            _ => (true, null, null)!
        };
    }
}