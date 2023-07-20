using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task AddRolUserForId(string userId);
        Task <IdentityUserRole>ValidateAccessAplicationUserRole(string userId);
    }
}
