using System.ComponentModel.DataAnnotations;

using MediatR;

using Identity.Dto.Response;

namespace Identity.Dto.Request.Query
{
    public class UserCredentialsQuery : IRequest<JwtResponse>
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
