using Identity.Dto.Request.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.EventHandlers.User
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand,Unit>
    {
        public async Task<Unit> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
           return Unit.Value;
        }
    }
}
