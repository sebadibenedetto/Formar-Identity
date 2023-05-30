using Identity.Dto.Request.Query;
using MediatR;

namespace Identity.Dto.Request.Command
{
    public record InsertUserCommand(string Nombre, string Email, string Password, string Username) : IRequest<ImpersonateUserQuery>;
    
}