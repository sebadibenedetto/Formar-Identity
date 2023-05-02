using Identity.Dto.Response;
using Identity.Dto.Request.Query;
using Identity.Domain.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Identity.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly ILoginBuilderDirectorService loginBuilderDirectorService;

        public UserService(ILoginBuilderDirectorService loginBuilderDirectorService)
        {
            this.loginBuilderDirectorService = loginBuilderDirectorService;
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
