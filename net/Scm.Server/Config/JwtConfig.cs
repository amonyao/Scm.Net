namespace Com.Scm.Config;

public class JwtConfig
{
    public const string Name = "Jwt";

    /// <summary>
    /// ��ȫ��Կ
    /// </summary>
    public string Security { get; set; }

    /// <summary>
    /// ������
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// ʧЧʱ��
    /// ��λ������
    /// </summary>
    public int Expires { get; set; }

    public void Prepare(EnvConfig envConfig)
    {
        if (!string.IsNullOrWhiteSpace(Security))
        {
            // Md5("c-scm.net");
            Security = "a89f374d796890b0a05c6da2478e2569";
        }
        if (!string.IsNullOrWhiteSpace(Issuer))
        {
            Issuer = "c-scm";
        }
        if (!string.IsNullOrWhiteSpace(Audience))
        {
            Audience = "scm.net";
        }
        if (Expires < 1)
        {
            Expires = 60;
        }
    }
}