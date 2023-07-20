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
        private readonly IRolRepository rolRepository;

        public UserService(ILoginBuilderDirectorService loginBuilderDirectorService, IRolRepository rolRepository)
        {
            this.loginBuilderDirectorService = loginBuilderDirectorService;
            this.rolRepository = rolRepository;
        }
        public async Task AddRolUser(string userId)
        {
            var user = await rolRepository.GetUserByIdAsync(userId);
            if (user == null) {
                throw new NotImplementedException("El usuario no existe");
            }
            var currentUserRole = await rolRepository.ValidateAccessAplicationUserRole(userId);
            if (currentUserRole != null)
            {
                throw new NotImplementedException("este usuario ya tiene un rol asignado");
            }
            await rolRepository.AddRolUserForId(user.Id);
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
