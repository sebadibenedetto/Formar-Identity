using Identity.Dto.Response;
using Identity.Dto.Request.Query;
using Identity.Domain.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Identity.Infrastructure.Repositories.Interfaces;
using Identity.Dto.Request.Command;

namespace Identity.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly ILoginBuilderDirectorService loginBuilderDirectorService;
        private readonly IUserRepository userRepository;

        public UserService(ILoginBuilderDirectorService loginBuilderDirectorService, IUserRepository userRepository)
        {
            this.loginBuilderDirectorService = loginBuilderDirectorService;
            this.userRepository = userRepository;
        }
        public async Task AddRolUser(InsertRoleCommand insertRoleCommand)
        {
            userRepository.AddRolUserForId(insertRoleCommand.UserId);
        }

        public async Task<JwtResponse> ImpersonateAsync(ImpersonateUserQuery query)
        {
            return await loginBuilderDirectorService.ImpersonateAsync(query.UserName);
        }

        public async Task<JwtResponse> LoginAsync(UserCredentialsQuery query)
        {
            return await loginBuilderDirectorService.LoginAsync(query.UserName, query.Password);
        }
    }
}
