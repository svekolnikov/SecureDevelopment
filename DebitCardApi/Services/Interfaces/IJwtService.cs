using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DebitCardApi.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(IList<Claim> claims);
        public JwtSecurityToken VerifyToken(string token);
    }
}
