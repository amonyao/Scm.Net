namespace Com.Scm.Jwt.Model;

public class JwtToken
{
    public long id { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public long user_id { get; set; }
    public string user_name { get; set; }

    /// <summary>
    /// 机构
    /// </summary>
    public long unit_id { get; set; }
    public string unit_name { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public string Role { get; set; }
    public string RoleArray { get; set; }

    public DateTime time { get; set; }
}