using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Entities
{
    [Table("Users")]
    public class User : IdentityUser
    {

    }
}