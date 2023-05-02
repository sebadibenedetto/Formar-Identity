using System.Threading.Tasks;
using System.Threading;

using Identity.Dto.Request.Query;
using Identity.Dto.Response;

namespace Identity.Api.Sdk.Interfaces
{
    public interface IAuthClient
    {
        Task<JwtResponse> LoginAsync(UserCredentialsQuery credentials, CancellationToken cancellationToken = default);
    }
}
