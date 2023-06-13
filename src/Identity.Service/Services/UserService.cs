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

        public async Task AddUser(InsertUserCommand insertUserCommand)
        {
            var user = new User();
            user.Email = insertUserCommand.Email;
            user.Name = insertUserCommand.Name;
            user.LastName = insertUserCommand.LastName;

            userRepository.Add(user);
        }
        //public async Task<bool> SearchUserForEmail(InsertUserCommand insertUserCommand)
        //{
        //    var email = insertUserCommand.Email;
        //    var resp = await userRepository.Search(email);
        //    if(resp == null)
        //    {
        //        return false;
        //    }
        //    return true;
            
        //}
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
