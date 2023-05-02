using System.Security.Claims;

namespace Identity.Sdk.Lib.Session
{
    public static class Fields
    {
        public const string Email = ClaimTypes.Email;
        public const string Id = ClaimTypes.NameIdentifier;
        public const string Role = ClaimTypes.Role;
        public const string Name = ClaimTypes.Name;
    }
}
