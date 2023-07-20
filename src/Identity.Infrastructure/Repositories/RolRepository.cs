using Identity.Data.Ef;
using Identity.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using Identity.Sdk.Lib.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        const string ACCESS_APLICATION = "AccessApplication";
        private readonly DataContext dbContext;
        private readonly UserManager<User> userManager;

        public RolRepository(DataContext dbContext, UserManager<User> userManager)
        {
            Argument.ThrowIfNull(dbContext, nameof(dbContext));
            Argument.ThrowIfNull(userManager, nameof(userManager));

            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task AddRolUserForId(string userId)
        {
            this.dbContext.UserRoles.Add(new IdentityUserRole
            {
                IsLocked = false,
                UserId = userId,
                RoleId = ACCESS_APLICATION
            });
            await this.dbContext.SaveChangesAsync();
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await this.dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IdentityUserRole> ValidateAccessAplicationUserRole(string userId)
        {
            return await this.dbContext.UserRoles.SingleOrDefaultAsync(x => x.UserId == userId && x.RoleId == ACCESS_APLICATION);
        }
    }
}
