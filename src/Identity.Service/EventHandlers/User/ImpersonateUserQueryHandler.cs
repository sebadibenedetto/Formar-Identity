using Identity.Domain.Service;
using Identity.Dto.Request.Query;
using Identity.Dto.Response;
using Identity.Sdk.Lib.Extensions;
using MediatR;

namespace Identity.Domain.EventHandlers.User
{
    public class ImpersonateUserQueryHandler : IRequestHandler<ImpersonateUserQuery, JwtResponse>
    {
        private readonly IUserService userService;

        public ImpersonateUserQueryHandler(IUserService userService)
        {
            Argument.ThrowIfNull(userService, nameof(userService));

            this.userService = userService;
        }

        public async Task<JwtResponse> Handle(ImpersonateUserQuery request, CancellationToken cancellationToken)
        {
            return await userService.ImpersonateAsync(request);
        }
    }
}
