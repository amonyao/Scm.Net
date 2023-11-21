using Com.Scm.Dsa.Cache;
using Com.Scm.Filter;
using Com.Scm.Image.SkiaSharp;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Api.Controllers;

/// <summary>
/// Captcha
/// </summary>
public class CaptchaController : ApiController
{
    private ICacheService _CacheService;

    public CaptchaController(ICacheService cacheService)
    {
        _CacheService = cacheService;
    }

    /// <summary>
    /// 生成图片
    /// </summary>
    /// <param name="identify">标识符</param>
    /// <returns>图片对象</returns>
    [HttpGet("{identify}"), AllowAnonymous, NoJsonResult, NoAuditLog]
    public async Task<IActionResult> Get(string identify)
    {
        if (string.IsNullOrEmpty(identify))
        {
            identify = ServerUtils.GetIp();
        }

        var result = new ImageEngine().GenCaptcha();
        _CacheService.SetCache(KeyUtils.CAPTCHACODE + identify, result.Value, 300);
        return File(result.Image, "image/gif");
    }
}