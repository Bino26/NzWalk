using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace NzWalkAPI.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public TokenRepository()
        {
            
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create clains

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            
        }
    }
}
