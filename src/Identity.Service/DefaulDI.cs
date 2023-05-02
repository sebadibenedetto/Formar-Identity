using System.Reflection;

using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Identity.Domain.Service;
using Identity.Infrastructure;
using Identity.Domain.Service.Interfaces;
using Identity.Domain.Services.Interfaces;
using Identity.Domain.Services;
using Identity.Data;

namespace Identity.Domain
{
    public static class DefaulDI
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginBuilderService, LoginBuilderService>();
            services.AddScoped<ILoginBuilderDirectorService, LoginBuilderDirectorService>();
            services.AddScoped<IUserService, UserService>();
            
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddContextDb(configuration);
            services.AddSetupIdentity();
            return services;
        }
    }
}
