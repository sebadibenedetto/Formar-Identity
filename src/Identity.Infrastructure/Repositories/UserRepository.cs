using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Identity.Data.Ef;
using Identity.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using Identity.Sdk.Lib.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dbContext;
        private readonly UserManager<User> userManager;

        public UserRepository(DataContext dbContext, UserManager<User> userManager)
        {
            Argument.ThrowIfNull(dbContext, nameof(dbContext));
            Argument.ThrowIfNull(userManager, nameof(userManager));

            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task Add(User user)
        {
            //await userManager.AddPasswordAsync(user, password);
            var datos = dbContext.Users.Add(user);
            //dbContext.SaveChanges();
        }

        public Task<bool> CheckHaveRolAsync(User user, string rol)
        {
            return this.userManager.IsInRoleAsync(user, rol);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await this.userManager.CheckPasswordAsync(user, password);
        }

        public async Task<User> FindAsync(string userName)
        {
            return await this.dbContext.Users.SingleOrDefaultAsync(x => x.UserName == userName || x.Email == userName);
        }
    }
}
