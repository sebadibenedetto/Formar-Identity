using MediatR;

using Identity.Api.Controllers;

namespace Identity.Api.Test.Controllers.Base
{
    public class ControllerTest<T>
        where T : BaseController
    {
        public T Sut { get; set; }
        public IMediator Mediator { get; set; }
    }
}
