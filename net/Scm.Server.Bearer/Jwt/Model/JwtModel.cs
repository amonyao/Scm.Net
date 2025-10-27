namespace Com.Scm.Jwt.Model;

/// <summary>
/// 
/// </summary>
public class JwtModel
{
    public const string Name = "JwtAuth";

    /// <summary>
    /// 
    /// </summary>
    public string Security { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// ʧЧʱ�䣬��λ������
    /// </summary>
    public int WebExp { get; set; }
}