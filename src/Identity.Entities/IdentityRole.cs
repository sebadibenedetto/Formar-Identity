using Microsoft.AspNetCore.Identity;

namespace Identity.Entities
{
    public class IdentityRole : IdentityRole<string>
    {
        public string Description { get; set; }
        public virtual ICollection<IdentityUserRole> UserRoles { get; set; }
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }
}
