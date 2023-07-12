using MediatR;
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
        [HttpPost("Rol")]
        public async Task<IActionResult> InsertRole([FromBody] InsertRoleCommand command)
        => Ok(await Mediator.Send(command));
    }
}
