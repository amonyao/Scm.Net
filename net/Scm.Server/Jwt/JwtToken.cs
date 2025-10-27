using Com.Scm.Enums;
using Com.Scm.Ur;

namespace Com.Scm.Jwt;

public class JwtToken
{
    /// <summary>
    /// Header中的token名称
    /// </summary>
    public static readonly string TokenName = "accessToken";

    /// <summary>
    /// 会话ID
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public long user_id { get; set; } = UserDto.SYS_ID;
    public string user_codes { get; set; }
    public string user_name { get; set; }

    /// <summary>
    /// 会话时间
    /// </summary>
    public DateTime time { get; set; }

    /// <summary>
    /// 数据权限
    /// </summary>
    public ScmUserDataEnum data { get; set; }

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(id);
    }

    public bool IsLogin()
    {
        return user_id > 0;
    }
}