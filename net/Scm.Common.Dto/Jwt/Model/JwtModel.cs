namespace Com.Scm.Jwt.Model;

public class JwtModel
{
    public const string Name = "JwtAuth";

    public string Security { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
    /// <summary>
    /// 失效时间，单位：分钟
    /// </summary>
    public int WebExp { get; set; }
}