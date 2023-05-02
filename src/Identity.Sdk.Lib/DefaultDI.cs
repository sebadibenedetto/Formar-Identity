using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

using Identity.Sdk.Lib.Extensions;
using Identity.Sdk.Lib.Jwt;
using Identity.Sdk.Lib.Session;

namespace Identity.Sdk.Lib
{
    public static class DefaultDI
    {
        public static void InitializeEncryption(this IServiceCollection services, IConfiguration configuration)
        {
            Encryption.Initialization(configuration.GetKeyVaultConfig("Cryptography:Key"), 
                configuration.GetKeyVaultConfig("Cryptography:IV"), 
                configuration.GetKeyVaultConfig("Cryptography:Salt"));
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration
            var secret = configuration.GetKeyVaultConfig("JwtOptions:Secret");

            services.Configure<JwtOptions>(m => new JwtOptions() { Secret = secret });
            services.AddScoped<IJwtFactory>(m => new JwtFactory(secret));

            //Session
            services.AddScoped<IUserSession, UserSession>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Policies
            services
                .AddMvcCore(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    opt.Filters.Add(new AuthorizeFilter(policy));

                })
                .AddApiExplorer()
                .AddDataAnnotations()
                .AddAuthorization();

            services.AddCors();

            //Authentication Configurations
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
