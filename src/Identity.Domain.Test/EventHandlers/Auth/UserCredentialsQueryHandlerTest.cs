using Identity.Domain.EventHandlers.Auth;
using Identity.Domain.Service;
using Identity.Dto.Request.Query;

namespace Identity.Domain.Test.EventHandlers.Auth
{
    public class UserCredentialsQueryHandlerTest
    {
        private UserCredentialsQueryHandler sut;

        private IUserService userService;

        public UserCredentialsQueryHandlerTest()
        {
            userService = Mock.Of<IUserService>();
        }

        public class TheConstructor : UserCredentialsQueryHandlerTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_dbContext_is_null()
            {
                // Arrange
                this.userService = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(nameof(userService),
                    () => new UserCredentialsQueryHandler(userService));
            }
        }

        public class TheMethod_Handle : UserCredentialsQueryHandlerTest 
        {
            [Fact]
            public async void Should_return_data_when_domain_services_return_ok()
            {
                // Arrange
                var expectedResult = new Dto.Response.JwtResponse
                {
                    Auth_Token = "testAuthToken",
                };

                Mock.Get(this.userService)
                    .Setup(c => c.LoginAsync(It.IsAny<UserCredentialsQuery>()))
                    .ReturnsAsync(expectedResult);

                this.sut = new UserCredentialsQueryHandler(this.userService);

                // Act 
                var result = await this.sut.Handle(It.IsAny<UserCredentialsQuery>(), default);

                //Assert
                Mock.Get(this.userService).Verify(c => c.LoginAsync(It.IsAny<UserCredentialsQuery>()), Times.Once);
                result.Should().BeEquivalentTo(expectedResult);
            }

            [Fact]
            public async void Should_return_exception_when_domain_services_return_error()
            {
                // Arrange
                var expectedResult = new Dto.Response.JwtResponse
                {
                    Auth_Token = "testAuthToken",
                };

                Mock.Get(this.userService)
                    .Setup(c => c.LoginAsync(It.IsAny<UserCredentialsQuery>()))
                    .Throws(new Exception(string.Empty));

                this.sut = new UserCredentialsQueryHandler(this.userService);

                // Act
                var result = await Record.ExceptionAsync(() => this.sut.Handle(It.IsAny<UserCredentialsQuery>(), default));

                // Assert
                result.Should().BeOfType<Exception>();
                result?.Message.Should().Be(string.Empty);
            }
        }
    }
}
