using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Dto.Request.Command
{
    public class InsertRoleCommand : IRequest<Unit>
    {
        [Required(ErrorMessage = "El id es requerido")]
        [DataType(DataType.Text)]
        public string UserId { get; set; }
    }
}
