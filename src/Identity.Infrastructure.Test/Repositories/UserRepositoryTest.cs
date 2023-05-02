using Identity.Data.Ef;
using Identity.Entities;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Test.Attributes;
using Identity.Infrastructure.Test.Customizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Test.Repositories
{
    public class UserRepositoryTest
    {
        private DataContext dbContext;
        private UserManager<User> userManager;

        public class TheConstructor : UserRepositoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_dbContext_is_null()
            {
                // Arrange
                this.dbContext = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(nameof(dbContext),
                    () => new UserRepository(
                    dbContext,
                    userManager));
            }

            [Fact]
            public void Should_throw_an_ArgumentNullException_when_userManager_is_null()
            {
                // Arrange
                DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
                this.dbContext = new DataContext(dbContextOptionsBuilder.Options);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(nameof(userManager),
                    () => new UserRepository(
                    dbContext,
                    userManager));
            }
        }

        public class TheMethod_FindAsync : UserRepositoryTest
        {
            [Theory]
            [DefaultData(typeof(DataContextCustomization))]
            public void Should_return_user_when_UserName_exists(
                UserRepository sut)
            {
                // Arrange
                var userNameEmail = "Paulina Ibarra";

                // Act
                var result = sut.FindAsync(userNameEmail);

                // Assert
                Assert.NotNull(result.Result);
                Assert.Equal(userNameEmail, result.Result.UserName);
            }

            [Theory]
            [DefaultData(typeof(DataContextCustomization))]
            public void Should_return_user_when_userEmail_exists(
                UserRepository sut)
            {
                // Arrange
                var userNameEmail = "juanperez@email.com";

                //Act
                var result = sut.FindAsync(userNameEmail);

                // Assert
                Assert.NotNull(result.Result);
                Assert.Equal(userNameEmail, result.Result.Email);
            }

            [Theory]
            [DefaultData(typeof(DataContextCustomization))]
            public void Should_return_user_when_userNameEmail_not_exists(
                UserRepository sut)
            {
                // Arrage
                var userNameEmail = "noUserEmail";

                // Act
                var result = sut.FindAsync(userNameEmail);

                // Assert
                Assert.Null(result.Result);
            }
        }
    }
}