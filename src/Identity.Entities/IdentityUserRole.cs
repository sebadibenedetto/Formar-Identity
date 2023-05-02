using Microsoft.AspNetCore.Identity;

namespace Identity.Entities
{
    public class IdentityUserRole : IdentityUserRole<string>
    {
        public override string RoleId { get; set; }
        public override string UserId { get; set; }
        public virtual IdentityRole Role { get; set; }
        public bool IsLocked { get; set; }
    }
}
