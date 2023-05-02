using MediatR;

using Identity.Api.Test.Controllers.Base;
using Identity.Dto.Request.Query;
using Identity.Api.Controllers;

namespace Identity.Api.Test.Controllers
{
    public class AuthControllerTest : ControllerTest<AuthController>
    {
        public AuthControllerTest()
        {
            this.Mediator = Mock.Of<IMediator>();

            this.Sut = new AuthController(this.Mediator);
        }

        public class The_Constructor : AuthControllerTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_mediator_is_null()
            {
                // Arrange
                this.Mediator = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>("mediator", 
                    () => new AuthController(this.Mediator));
            }
        }

        public class TheMethod_LoginAsync : AuthControllerTest
        {
            UserCredentialsQuery UserCredentialsQuery { get; set; }
            public TheMethod_LoginAsync()
            {
                this.UserCredentialsQuery = new UserCredentialsQuery();
            }

            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // Act
                await this.Sut.LoginAsync(UserCredentialsQuery);

                // Assert
                Mock.Get(this.Mediator).Verify(
                    mdt => mdt.Send(
                            It.Is<UserCredentialsQuery>(
                                m => m.UserName == this.UserCredentialsQuery.UserName),
                            CancellationToken.None),
                    Times.Once());
            }
        }
    }
}
