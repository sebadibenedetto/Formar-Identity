using Identity.Sdk.Lib.Jwt;
using Identity.Sdk.Lib.Session;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Identity.Sdk.Lib.Test.Jwt
{
    public class JwtFactoryTest
    {
        private JwtFactory jwtFactory;
        private const string Key = "development_secret_key";
        public JwtFactoryTest() 
        {
            this.jwtFactory = new JwtFactory(Key);
        }

        public class TheConstructor_With_JwtOptionsAccesor : JwtFactoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_jwtOptionsAccesor_parameter_is_null()
            {
                // arrange
                IOptions<JwtOptions> jwtOptionsAccesor = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(jwtOptionsAccesor),
                    () => new JwtFactory(jwtOptionsAccesor));
            }
        }
        public class TheConstructor_With_JwtOptions : JwtFactoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_jwtOptions_parameter_is_null()
            {
                // arrange
                JwtOptions jwtOptions = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(jwtOptions),
                    () => new JwtFactory(jwtOptions));
            }
        }

        public class TheMethod_GenerateJwt : JwtFactoryTest
        {
            [Fact]
            public async Task Should_Return_AccessToken_NotNull()
            {
                // arrange
                var claims = new List<System.Security.Claims.Claim> 
                { 
                    new System.Security.Claims.Claim(Fields.Role, Roles.AccessApplication)
                };

                var id = "rDqxn5D2RsW2YUhJrBoUOw";

                // act
                var result = await jwtFactory.GenerateJwt(id, claims);

                // assert
                Assert.NotEmpty(result.Auth_Token);
            }
        }

        public class TheMethod_ValidateToken : JwtFactoryTest 
        {
            [Fact]
            public async Task Should_Validate_AccessToken_NotNull()
            {
                // arrange
                var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWNjZXNzQXBwbGljYXRpb24iLCJuYmYiOjE2NjczNDY3MTMsImV4cCI6MTY2NzM1MzkxMywiaWF0IjoxNjY3MzQ2NzEzfQ.oOcMxc-xFGHGpcMM-HPDNnN1Z4nG6YA00q6xAUTxuv4";

                // act
                var result = jwtFactory.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
                    ValidateLifetime = false // we check expired tokens here
                });

                // assert
                Assert.NotNull(result);
                Assert.NotNull(result.Identity);
                Assert.True(result.Identity.IsAuthenticated);
            }
        }
    }
}
