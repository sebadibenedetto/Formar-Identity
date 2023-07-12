using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Identity.Data.Ef;
using Identity.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using Identity.Sdk.Lib.Extensions;

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
        public async Task AddRolUserForId(string id)
        {
            var user = await this.dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotImplementedException("No existe ese usuario");
            }
        }
    }
}
