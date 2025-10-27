namespace Com.Scm.Jwt.Model;

public class JwtToken
{
    public long id { get; set; }

    /// <summary>
    /// �û�
    /// </summary>
    public long user_id { get; set; }
    public string user_name { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public long unit_id { get; set; }
    public string unit_name { get; set; }

    /// <summary>
    /// ��ɫ
    /// </summary>
    public string Role { get; set; }
    public string RoleArray { get; set; }

    public DateTime time { get; set; }
}