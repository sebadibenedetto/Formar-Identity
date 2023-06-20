using Identity.Dto.Request.Command;
using Identity.Dto.Request.Query;
using Identity.Dto.Response;

namespace Identity.Domain.Service
{
    public interface IUserService
    {
        Task AddUser(InsertUserCommand insertUserCommand);
        Task<JwtResponse> ImpersonateAsync(ImpersonateUserQuery request);
        Task<JwtResponse> LoginAsync(UserCredentialsQuery query);
    }
}
