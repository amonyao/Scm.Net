using Com.Scm.Express.Dto;

namespace Com.Scm.Express.My.Shipper.SF.Exp.Auth
{
    public class AuthRequest : SfExpRequest
    {
        public override string GetServicePath()
        {
            if (MyExpress.Environment == EnvironmentEnum.Release)
            {
                return "https://sfapi.sf-express.com/oauth2/accessToken";
            }

            return "https://sfapi-sbox.sf-express.com/oauth2/accessToken";
        }

        public override string GetServiceCode()
        {
            return "";
        }
    }
}
