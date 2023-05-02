using MediatR;

using Identity.Dto.Response;


namespace Identity.Dto.Request.Query
{
    public class ImpersonateUserQuery : IRequest<JwtResponse>
    {
        public string UserName { get; set; }
    }
}
