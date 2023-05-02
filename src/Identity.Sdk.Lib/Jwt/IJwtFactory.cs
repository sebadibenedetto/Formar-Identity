using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using Identity.Dto.Response;

namespace Identity.Sdk.Lib.Jwt
{
    public interface IJwtFactory
    {
        Task<JwtResponse> GenerateJwt(string identifier, IEnumerable<Claim> claims);
        ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
    }
}
