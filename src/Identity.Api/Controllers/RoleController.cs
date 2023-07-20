using Identity.Dto.Request.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddAccessAplicationRole")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertRoleAsync([FromBody] InsertRoleCommand command)
        => Ok(await Mediator.Send(command));
    }
}
