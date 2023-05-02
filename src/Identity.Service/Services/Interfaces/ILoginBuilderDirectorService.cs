using Identity.Dto.Response;

namespace Identity.Domain.Services.Interfaces
{
    public interface ILoginBuilderDirectorService
    {
        Task<JwtResponse> ImpersonateAsync(string userName);
        Task<JwtResponse> LoginAsync(string userName, string password);
    }
}
