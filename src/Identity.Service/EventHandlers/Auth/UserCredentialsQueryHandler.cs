using MediatR;

using Identity.Domain.Service;
using Identity.Dto.Request.Query;
using Identity.Dto.Response;
using Identity.Sdk.Lib.Extensions;

namespace Identity.Domain.EventHandlers.Auth
{
    public class UserCredentialsQueryHandler : IRequestHandler<UserCredentialsQuery, JwtResponse>
    {
        private readonly IUserService userService;

        public UserCredentialsQueryHandler(IUserService userService)
        {
            Argument.ThrowIfNull(userService, nameof(userService));

            this.userService = userService;
        }

        public async Task<JwtResponse> Handle(UserCredentialsQuery request, CancellationToken cancellationToken)
        {
            return await userService.LoginAsync(request);
        }
    }
}