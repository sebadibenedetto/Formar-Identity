using Identity.Entities;

namespace Identity.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CheckHaveRolAsync(User user, string rol);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<User> FindAsync(string userName);
        Task AddRolUserForId(string id);
    }
}
