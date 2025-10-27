namespace Com.Scm.Jwt
{
    public interface IJwtHolder
    {
        void SetToken(JwtToken token);

        JwtToken GetToken();

        void Clear();
    }
}
