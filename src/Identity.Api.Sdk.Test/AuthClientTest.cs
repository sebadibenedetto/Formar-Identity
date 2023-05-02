using Identity.Api.Sdk.Test.Extensions;
using Identity.Dto.Request.Query;
using Identity.Dto.Response;

namespace Identity.Api.Sdk.Test
{
    public class AuthClientTest
    {
        private string BaseAddress => "http://test.com/api/";

        private HttpMessageHandler HandlerMessage { get; set; }

        private AuthClient Sut { get; set; }

        public AuthClientTest()
        {
            this.HandlerMessage = Mock.Of<HttpMessageHandler>();

            var httpClient = new HttpClient(this.HandlerMessage)
            {
                BaseAddress = new Uri(this.BaseAddress)
            };

            this.Sut = new AuthClient(httpClient);
        }

        public class TheConstructor : AuthClientTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_the_supplied_httpClient_is_null()
            {
                // Arrange
                HttpClient httpClient = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(nameof(httpClient), () => new AuthClient(httpClient));
            }
        }
        public class TheMethod_LoginAsync : AuthClientTest
        {
            [Fact]
            public async Task Should_call_its_httpClient_with_proper_url_and_cancellationToken_supplied()
            {
                // Arrange
                var jwt = new JwtResponse()
                {
                    Id = "testJwt",
                    Auth_Token = "TokenTest"
                };

                Mock.Get(this.HandlerMessage).SetupPostReturnCodeWithValue(jwt);

                var expectedUri = $"{this.BaseAddress}Auth/login";

                // Act
                var response = await this.Sut.LoginAsync(new UserCredentialsQuery());

                // Assert
                Mock.Get(this.HandlerMessage).VerifyCalledWithPostMethod(expectedUri);
                Assert.Equal(jwt.Id, response.Id);
                Assert.Equal(jwt.Auth_Token, response.Auth_Token);
            }
        }
    }
}