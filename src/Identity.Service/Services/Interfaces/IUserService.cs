using Identity.Dto.Request.Query;
using Identity.Dto.Response;

namespace Identity.Domain.Service
{
    public interface IUserService
    {
        Task<JwtResponse> ImpersonateAsync(ImpersonateUserQuery request);
        Task<JwtResponse> LoginAsync(UserCredentialsQuery query);
    }
}
