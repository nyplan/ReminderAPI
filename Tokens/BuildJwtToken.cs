using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ReminderAPI.Tokens
{
    public class BuildJwtToken
    {
        public string CreateToken()
        {
            var bytes = Encoding.UTF8.GetBytes("reminderapijwttoken");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddMinutes(1), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
