using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Identity.Data.Ef;

namespace Identity.Data
{
    public static class DefaultDI
    {
        public static IServiceCollection AddContextDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt =>
                        opt.EnableSensitiveDataLogging()
                            .AddInterceptors(new ContextInterceptor(new HttpContextAccessor()))
                            .UseOpenIddict()
                            .UseSqlServer(configuration.GetConnectionString("DataContext")),
                                          ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddSetupIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Entities.User, Entities.IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = false;
            })
           .AddEntityFrameworkStores<DataContext>()
           .AddErrorDescriber<IdentityErrorDescriber>()
           .AddDefaultTokenProviders();

            return services;
        }
    }
}
