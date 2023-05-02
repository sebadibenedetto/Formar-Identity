using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.Sdk.Lib.Extensions;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator;

        protected BaseController(IMediator mediator)
        {
            Argument.ThrowIfNull(mediator, nameof(mediator));

            this.Mediator = mediator;
        }
    }
}
