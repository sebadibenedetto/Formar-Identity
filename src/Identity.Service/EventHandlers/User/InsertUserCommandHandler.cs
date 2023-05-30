using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.EventHandlers.User
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, string>
    {   
        public Task<string> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
