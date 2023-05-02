using AutoFixture;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;

using Identity.Data.Ef;
using Identity.Entities;

namespace Identity.Infrastructure.Test.Customizations
{
    internal class DataContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            const int id = 1;

            var identityUserRole1 = new IdentityUserRole
            {
                UserId = "UserId",
                RoleId = "RoleId1",
                IsLocked = false,
            };

            var identityUserRole2 = new IdentityUserRole
            {
                UserId = "UserId",
                RoleId = "RoleId2",
                IsLocked = false,
            };

            var userRoles = new List<IdentityUserRole>
            {
                identityUserRole1,
                identityUserRole2
            };

            var applicationUserM = new User
            {
                Id = "JuanPerez",
                UserName = "Juan Perez",
                Email = "juanperez@email.com"
            };

            var applicationUserWithoutCuit = new User
            {
                Id = "RaulGonzales",
                UserName = "Raul Gonzalez",
                Email = "raulgonzalez@email.com"
            };

            var applicationUserF = new User
            {
                Id = "PaulinaIbarra",
                UserName = "Paulina Ibarra",
                EmailConfirmed = true,
                Email = "paulinaibarra@email.com"
            };

            var userLogins = new List<IdentityUserLogin>
            {
                new IdentityUserLogin
                {
                    LoginProvider = "GOOGLE",
                    ProviderDisplayName = "paulinaibarra@email.com",
                    ProviderKey = "AAAA",
                    UserId = "UserId"
                }
            };

            var applicationUsers = new List<User>
            {
                applicationUserM,
                applicationUserF,
                applicationUserWithoutCuit
            };

            var dbSetUser = applicationUsers.AsQueryable().BuildMockDbSet();
            var dbSetUserLogins = userLogins.AsQueryable().BuildMockDbSet();
            var dbSetUserRoles = userRoles.AsQueryable().BuildMockDbSet();            

            var options = new DbContextOptionsBuilder<DataContext>().Options;

            var ctx = new Mock<DataContext>(options);
            ctx.Setup(d => d.UserRoles).Returns(dbSetUserRoles.Object);
            ctx.Setup(d => d.UserLogins).Returns(dbSetUserLogins.Object);
            ctx.Setup(d => d.Users).Returns(dbSetUser.Object);

            fixture.Register(() => ctx.Object);
        }
    }
}
