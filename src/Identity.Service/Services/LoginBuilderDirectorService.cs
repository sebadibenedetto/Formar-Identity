using Identity.Domain.Service.Interfaces;
using Identity.Domain.Services.Interfaces;
using Identity.Dto.Response;
using Identity.Sdk.Lib.Extensions;

namespace Identity.Domain.Services
{
    public class LoginBuilderDirectorService : ILoginBuilderDirectorService
    {
        private ILoginBuilderService loginBuilderService { get; set; }

        public LoginBuilderDirectorService(ILoginBuilderService loginBuilderService)
        {
            Argument.ThrowIfNull(loginBuilderService, nameof(loginBuilderService));

            this.loginBuilderService = loginBuilderService;
        }

        public async Task<JwtResponse> LoginAsync(string userName, string password) 
        {
            await loginBuilderService.WithUser(userName);
            //await loginBuilderService.WithValidatePassword(password);  //DESCOMENTAR AL AGREGAR EL ADD USER
            await loginBuilderService.WithAccessApplication();
            loginBuilderService.WithClaims();

            return await loginBuilderService.GetJwtAsync();
        }

        public async Task<JwtResponse> ImpersonateAsync(string userName)
        {
            await loginBuilderService.WithUser(userName);
            await loginBuilderService.WithAccessApplication();
            loginBuilderService.WithClaims();

            return await loginBuilderService.GetJwtAsync();
        }
    }
}
