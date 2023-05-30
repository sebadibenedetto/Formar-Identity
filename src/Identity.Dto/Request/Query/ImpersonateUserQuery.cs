using MediatR;

using Identity.Dto.Response;


namespace Identity.Dto.Request.Query
{
    public class ImpersonateUserQuery : IRequest<JwtResponse>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }

    }
}
