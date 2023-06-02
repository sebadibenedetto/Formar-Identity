using Identity.Dto.Request.Query;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Dto.Request.Command
{
    public class InsertUserCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
    
}