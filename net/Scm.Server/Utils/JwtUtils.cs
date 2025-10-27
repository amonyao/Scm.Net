using Com.Scm.Config;
using Com.Scm.Enums;
using Com.Scm.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Com.Scm.Utils;

public class JwtUtils
{
    public static string IssueJwt(JwtToken token)
    {
        var jwtModel = AppUtils.GetConfig<JwtConfig>(JwtConfig.Name);

        var claims = new List<Claim>();
        //每次登陆动态刷新
        //JwtConst.ValidAudience = token.Id + DateTime.Now.ToString(CultureInfo.InvariantCulture);
        claims.AddRange(new[] {
            new Claim (nameof (JwtToken.id), token.id.ToString()),
            new Claim (nameof (JwtToken.user_id), token.user_id.ToString()),
            new Claim (nameof (JwtToken.user_codes), token.user_codes??""),
            new Claim (nameof (JwtToken.user_name), token.user_name??""),
            new Claim (nameof (JwtToken.time), token.time.ToBinary().ToString()),
            new Claim (nameof (JwtToken.data), ((int)token.data).ToString()),
            //new Claim (ClaimTypes.Role, token.Role??""),
            //new Claim (nameof (JwtToken.RoleArray), token.RoleArray),
        });
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtModel.Security));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            issuer: jwtModel.Issuer,
            audience: jwtModel.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtModel.Expires),
            signingCredentials: cred);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="jwtStr"></param>
    /// <returns></returns>
    public static JwtToken SerializeJwt(string jwtStr)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(jwtStr);
        object id;
        object userId;
        object userCodes;
        object userName;
        object time;
        object data;
        //object role;
        //object roleArray;

        try
        {
            jwtToken.Payload.TryGetValue(nameof(JwtToken.id), out id);
            jwtToken.Payload.TryGetValue(nameof(JwtToken.user_id), out userId);
            jwtToken.Payload.TryGetValue(nameof(JwtToken.user_codes), out userCodes);
            jwtToken.Payload.TryGetValue(nameof(JwtToken.user_name), out userName);
            jwtToken.Payload.TryGetValue(nameof(JwtToken.time), out time);
            jwtToken.Payload.TryGetValue(nameof(JwtToken.data), out data);
            //jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            //jwtToken.Payload.TryGetValue("RoleArray", out roleArray);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new JwtToken()
        {
            id = id?.ToString(),
            user_id = Convert.ToInt64(userId),
            user_codes = userCodes?.ToString(),
            user_name = userName?.ToString(),
            time = DateTime.FromBinary(Convert.ToInt64(time)),
            data = (ScmUserDataEnum)Convert.ToInt32(data),
            //RoleArray = roleArray?.ToString(),
            //Role = role?.ToString()
        };
    }
}