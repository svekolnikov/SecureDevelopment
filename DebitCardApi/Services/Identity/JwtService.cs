using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DebitCardApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DebitCardApi.Services.Identity
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(IList<Claim> claims)
        {
            var signingCredentials = new SigningCredentials(Constants.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = Constants.ISSUER,
                Audience = Constants.AUDIENCE,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(Constants.LIFETIME),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenJson;
        }

        public JwtSecurityToken VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenBytes = Encoding.ASCII.GetBytes(Constants.KEY);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(tokenBytes),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var securityToken);

            return (JwtSecurityToken)securityToken;
        }
    }
}
