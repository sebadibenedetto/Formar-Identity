using Identity.Dto.Response;
using Identity.Dto.Request.Query;
using Identity.Domain.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Identity.Dto.Request.Command;
using Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Repositories.Interfaces;

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

        public async Task AddUser(InsertUserCommand insertUserCommand)
        {
            var user = new User();
            user.Name = insertUserCommand.Name;
            user.LastName = insertUserCommand.LastName;
            user.UserName = user.Name;
            user.Email = insertUserCommand.Email;
            user.NormalizedEmail = user.Email;
            userRepository.Add(user,insertUserCommand.Password);    
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
