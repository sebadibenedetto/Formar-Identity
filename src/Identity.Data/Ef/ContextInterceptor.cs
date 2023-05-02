using System.Data.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Identity.Data.Ef
{
    public class ContextInterceptor : DbConnectionInterceptor
    {
        private IHttpContextAccessor httpContextAccessor;
        public ContextInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData, CancellationToken cancellationToken = default)
        {
            string user = !string.IsNullOrWhiteSpace(httpContextAccessor.HttpContext.User.Identity.Name) ? httpContextAccessor.HttpContext.User.Identity.Name : "NULL";
            
            SaveContext(connection, "currentUser", user);

            return base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
        }

        private void SaveContext(DbConnection connection, string key, string value)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            string command = string.Format("EXEC sp_set_session_context  @key = '{0}', @value = '{1}', @read_only = {2}", key, value, "1");
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
        }
    }
}
