using Com.Scm.Enums;
using Com.Scm.Ur;

namespace Com.Scm.Jwt;

public class JwtToken
{
    /// <summary>
    /// Header�е�token����
    /// </summary>
    public static readonly string TokenName = "accessToken";

    /// <summary>
    /// �ỰID
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// �û�
    /// </summary>
    public long user_id { get; set; } = UserDto.SYS_ID;
    public string user_codes { get; set; }
    public string user_name { get; set; }

    /// <summary>
    /// �Ựʱ��
    /// </summary>
    public DateTime time { get; set; }

    /// <summary>
    /// ����Ȩ��
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