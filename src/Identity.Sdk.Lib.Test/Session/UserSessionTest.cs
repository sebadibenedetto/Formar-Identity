using System.Security.Claims;

using FluentAssertions;
using Microsoft.AspNetCore.Http;

using Identity.Sdk.Lib.Session;

namespace Identity.Sdk.Lib.Test.Session
{
    public class UserSessionTest
    {
        public HttpContext HttpContext { get; set; }
        public UserSession Sut { get; set; }
        public UserSessionTest()
        {
            this.HttpContext = Mock.Of<HttpContext>();
            var httpContextAccessor = Mock.Of<IHttpContextAccessor>(a => a.HttpContext == this.HttpContext);

            this.Sut = new UserSession(httpContextAccessor);
        }

        private void MockClaims(IEnumerable<Claim> claims)
        {
            Mock.Get(this.HttpContext).Setup(c => c.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        }

        public class TheMethod_GetToken : UserSessionTest
        {
            [Fact]
            public void Should_return_authorizationToken_when_exists()
            {
                var httpRequest = new Mock<HttpRequest>();
                httpRequest.Setup(x => x.Headers).Returns(
                    new HeaderDictionary {
                {"Authorization", "Bearer token"}
                });

                var httpContext = new Mock<HttpContext>();
                httpContext.Setup(x => x.Request).Returns(httpRequest.Object);

                var httpContextAccessor = new Mock<IHttpContextAccessor>();
                httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);

                var sut = new UserSession(httpContextAccessor.Object);

                // Act
                var token = sut.GetToken();

                // Assert
                Assert.Equal("token", token);
            }

            [Fact]
            public void Should_return_emptu_when_autorizationToken_not_exists()
            {
                var httpRequest = new Mock<HttpRequest>();
                httpRequest.Setup(x => x.Headers).Returns(
                    new HeaderDictionary {
                {"Token", "Bearer token"}
                });

                var httpContext = new Mock<HttpContext>();
                httpContext.Setup(x => x.Request).Returns(httpRequest.Object);

                var httpContextAccessor = new Mock<IHttpContextAccessor>();
                httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);

                var sut = new UserSession(httpContextAccessor.Object);

                // Act
                var token = sut.GetToken();

                // Assert
                Assert.Empty(token);
            }
        }

        public class TheProperty_Id : UserSessionTest
        {
            [Fact]
            public void Should_return_claim_id_as_string()
            {
                // Arrange
                var expected = Guid.NewGuid().ToString();
                this.MockClaims(new[] { new Claim(Fields.Id, expected) });

                // Act
                var actual = this.Sut.Id;

                // Assert
                actual.Should().Be(expected);
            }
        }

        public class TheProperty_Email : UserSessionTest
        {
            [Fact]
            public void Should_return_claim_Email_as_string()
            {
                // Arrange
                var expected = "an-email@test.com";
                this.MockClaims(new[] { new Claim(Fields.Email, expected) });

                // Act
                var actual = this.Sut.Email;

                // Assert
                actual.Should().Be(expected);
            }
        }

        public class TheProperty_Roles : UserSessionTest
        {
            [Theory]
            [InlineData(Roles.AccessApplication)]
            [InlineData(Roles.AccessApplication, Roles.Admin)]
            [InlineData(Roles.AccessApplication, Roles.Admin, Roles.IdentityVerify)]
            public void Should_return_claim_Roles_as_enumerable_of_string(params string[] expected)
            {
                // Arrange
                this.MockClaims(expected.Select(role => new Claim(Fields.Role, role)));

                // Act
                var actual = this.Sut.Roles;

                // Assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}