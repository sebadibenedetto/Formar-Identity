using Microsoft.Extensions.Configuration;

namespace Identity.Sdk.Lib.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetKeyVaultConfig(this IConfiguration configuration, string configKey)
        {
            return configuration[configKey];
        }
    }
}
