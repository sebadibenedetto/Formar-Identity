using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace Identity.Sdk.Lib.Session
{
    public class UserSession : IUserSession
    {
        public string Id { get { return GetSingleValue<string>(Fields.Id); } }
        public string Email { get { return GetSingleValue<string>(Fields.Email); } }
        public IEnumerable<string> Roles { get { return GetEnumerableValue<string>(Fields.Role); } }

        private readonly IHttpContextAccessor httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var value = httpContext.Request.Headers.TryGetValue("Authorization", out var token);

            if (value)
                return token.ToString().Split(' ')[1];

            return string.Empty;
        }

        private T GetSingleValue<T>(string name)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext.User is ClaimsPrincipal claimsPrincipal)
            {
                Claim claim = claimsPrincipal.Claims.SingleOrDefault(clm => clm.Type == name);

                if (claim != null)
                {
                    return ConvertClaimValue<T>(claim);
                }
            }

            return default;
        }

        private IEnumerable<T> GetEnumerableValue<T>(string name)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext.User is ClaimsPrincipal claimsPrincipal)
            {
                IEnumerable<Claim> claims = claimsPrincipal.Claims.Where(clm => clm.Type == name);

                return claims.Select(claim => ConvertClaimValue<T>(claim));
            }

            return default;
        }

        private T ConvertClaimValue<T>(Claim claim)
        {
            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }
    }
}
