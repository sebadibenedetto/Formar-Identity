using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Identity.Dto.Request.Query;
using Identity.Sdk.Lib.Session;
using Identity.Entities;
using Identity.Api.Commands;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("impersonate")]
        //public async Task<IActionResult> ImpersonateAsync([FromBody] ImpersonateUserQuery impersonateQuery)
        //=> Ok(await Mediator.Send(impersonateQuery));
        public Task<string> Index([FromBody] InsertUserCommand command)
        {
            return Mediator.Send(command);
        }
    }
}
