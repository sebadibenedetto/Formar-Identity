using Identity.Dto.Request.Query;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Dto.Request.Command
{
    public class InsertUserCommand : IRequest<Unit>
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [DataType(DataType.Text)]
        [RegularExpression("^([a-zA-Z'])+$", ErrorMessage = "El nombre tiene que ser un string.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El username es requerido")]
        [DataType(DataType.Text)]
        [RegularExpression("^([a-zA-Z'])+$", ErrorMessage = "El nombre tiene que ser un string.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "debe ser un email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es requerido")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña debe tiene un mínimo de 8 caracteres")]
        public string Password { get; set; }
    }
    
}