﻿using Identity.Data.Ef;
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
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand,Unit>
    {
        private readonly IUserService userService;

        public InsertUserCommandHandler(IUserService userService)
        {
            Argument.ThrowIfNull(userService, nameof(userService));

            this.userService = userService;
        }
        public async Task<Unit> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            //userService.SearchUserForEmail(request);
            //userService.AddUser(request);
            

            return Unit.Value;
        }
    }
}
