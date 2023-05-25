using MediatR;
using System;

namespace Identity.Api.Commands
{
    public class InsertUserCommand : IRequest<string>{}

    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand,string>
    {
        public Task<string> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
    
}