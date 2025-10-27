using Com.Scm.Jwt.Model;

namespace Com.Scm.Jwt;

/// <summary>
/// jwt上下文
/// </summary>
public class JwtContextHolder
{
    /// <summary>
    /// 支持父子线程数据传递
    /// </summary>
    private readonly ThreadLocal<JwtToken> _threadLocalTenant = new();

    /// <summary>
    /// 设置租户ID
    /// </summary>
    /// <param name="token"></param>
    public void SetToken(JwtToken token)
    {
        _threadLocalTenant.Value = token;
    }

    /// <summary>
    /// 获取租户ID
    /// </summary>
    /// <returns></returns>
    public JwtToken GetToken()
    {
        try
        {
            return _threadLocalTenant.Value ?? new JwtToken();
        }
        catch
        {
            return new JwtToken();
        }
    }

    /// <summary>
    /// 清除tenantId
    /// </summary>
    public void Clear()
    {
        _threadLocalTenant.Dispose();
    }
}