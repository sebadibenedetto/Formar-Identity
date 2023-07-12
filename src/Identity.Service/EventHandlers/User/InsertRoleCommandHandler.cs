using Identity.Domain.Service;
using Identity.Dto.Request.Command;
using Identity.Sdk.Lib.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.EventHandlers.User
{
    public class InsertRoleCommandHandler : IRequestHandler<InsertRoleCommand,Unit>
    {
        private readonly IUserService userService;

        public InsertRoleCommandHandler(IUserService userService)
        {
            Argument.ThrowIfNull(userService, nameof(userService));
            this.userService = userService;
        }
        public async Task<Unit> Handle(InsertRoleCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
