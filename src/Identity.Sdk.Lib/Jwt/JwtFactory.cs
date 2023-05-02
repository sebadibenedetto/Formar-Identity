using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Identity.Dto.Response;

namespace Identity.Sdk.Lib.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtOptions jwtOptions;
        private readonly int expires = 120;

        public JwtFactory(string secret)
        {
            jwtOptions = new JwtOptions();

            jwtOptions.Secret = secret;
            jwtOptions.SigningCredentials = new SigningCredentials(
                                                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)), SecurityAlgorithms.HmacSha256);
            ThrowIfInvalidOptions(jwtOptions);
        }
        public JwtFactory(IOptions<JwtOptions> jwtOptionsAccesor)
        {
            jwtOptions = jwtOptionsAccesor?.Value ?? throw new ArgumentNullException(nameof(jwtOptionsAccesor));
            jwtOptions.SigningCredentials = new SigningCredentials(
                                                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)), SecurityAlgorithms.HmacSha256);
            ThrowIfInvalidOptions(jwtOptions);
        }
        public JwtFactory(JwtOptions jwtOptions)
        {
            this.jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
            this.jwtOptions.SigningCredentials = new SigningCredentials(
                                                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.jwtOptions.Secret)), SecurityAlgorithms.HmacSha256);
            ThrowIfInvalidOptions(this.jwtOptions);
        }

        public async Task<JwtResponse> GenerateJwt(string identifier, IEnumerable<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims);
            var jwt = new JwtResponse
            {
                Id = identifier,
                Auth_Token = await GenerateEncodedToken(identifier, claimsIdentity),
                Expires_In = expires,
                Refresh_Token = GenerateToken()
            };

            return jwt;
        }

        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var outBoundClaimTypeMap = tokenHandler.OutboundClaimTypeMap;
            outBoundClaimTypeMap.Add("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone", "mobilephone");

            var key = jwtOptions.Secret;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        private static void ThrowIfInvalidOptions(JwtOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtOptions.JtiGenerator));
            }
        }
        
        private string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }        
    }
}