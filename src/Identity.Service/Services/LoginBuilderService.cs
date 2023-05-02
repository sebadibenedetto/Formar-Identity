using Identity.Domain.Keys;
using Identity.Domain.Service.Interfaces;
using Identity.Dto.Response;
using Identity.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using Identity.Sdk.Lib.Exception;
using Identity.Sdk.Lib.Extensions;
using Identity.Sdk.Lib.Jwt;
using Identity.Sdk.Lib.Session;
using System.Security.Claims;

namespace Identity.Domain.Service
{
    public class LoginBuilderService : ILoginBuilderService
    {
        public User User { get; set; }
        public List<Claim> Claims { get; set; }

        private readonly IUserRepository userRepository;

        private readonly IJwtFactory jwtFactory;

        public LoginBuilderService(IUserRepository userRepository, IJwtFactory jwtFactory)
        {
            Argument.ThrowIfNull(userRepository, nameof(userRepository));
            Argument.ThrowIfNull(jwtFactory, nameof(jwtFactory));

            this.userRepository = userRepository;
            this.jwtFactory = jwtFactory;
            Claims = new List<Claim>();
        }

        public async Task<ILoginBuilderService> WithUser(string userName)
        {
            User = await this.userRepository.FindAsync(userName);

            if (User == null)
            {
                throw new InvalidUserCredentialsException();
            }

            return this;
        }

        public async Task<ILoginBuilderService> WithValidatePassword(string password)
        {
            if (!await this.userRepository.CheckPasswordAsync(this.User, password))
            {
                User = null;
                throw new InvalidUserCredentialsException();
            }

            return this;
        }

        public async Task<ILoginBuilderService> WithAccessApplication()
        {
            if (User is null || !await this.userRepository.CheckHaveRolAsync(User, Sdk.Lib.Session.Roles.AccessApplication)) 
            {
                User = null;
                var message = String.Format(Domain.Globalization.Message.RolesNotFound, Sdk.Lib.Session.Roles.AccessApplication);
                throw new InvalidUserCredentialsException(message);
            }

            return this;
        }

        public ILoginBuilderService WithClaims()
        {
            if (User is null)
            {
                User = null;
                throw new InvalidUserCredentialsException();
            }

            Claims.Add(new Claim(Fields.Role, Roles.AccessApplication));
            Claims.Add(new Claim(Fields.Id, User.Id));
            Claims.Add(new Claim(Fields.Name, User.UserName));
            if (!string.IsNullOrWhiteSpace(User.Email))
                Claims.Add(new Claim(Fields.Email, User.Email));

            return this;
        }

        public async Task<JwtResponse> GetJwtAsync()
        {
            var userKey = GetIdentifier();

            return await this.jwtFactory.GenerateJwt(userKey, Claims);
        }

        private string GetIdentifier()
        {
            return new UserKey { UserId = User.Id }.GetKey();
        }
    }
}
