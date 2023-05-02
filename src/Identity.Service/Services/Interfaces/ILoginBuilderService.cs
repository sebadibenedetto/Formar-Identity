using Identity.Dto.Response;

namespace Identity.Domain.Service.Interfaces
{
    public interface ILoginBuilderService
    {
        Task<ILoginBuilderService> WithUser(string userName);
        Task<ILoginBuilderService> WithValidatePassword(string password);
        Task<ILoginBuilderService> WithAccessApplication();
        ILoginBuilderService WithClaims();
        Task<JwtResponse> GetJwtAsync();
    }
}
