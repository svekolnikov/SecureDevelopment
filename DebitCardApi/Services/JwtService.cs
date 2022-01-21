using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DebitCardApi.Services.Interfaces;

namespace DebitCardApi.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(IList<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public JwtSecurityToken VerifyToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
