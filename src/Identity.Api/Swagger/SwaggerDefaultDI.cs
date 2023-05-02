using System.Reflection;

using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

using Identity.Api.Swagger;

namespace Identity.Api.Swagger
{
    public static class SwaggerDefaultDI
    {
        private const string SwaggerDocVersion = "v1";
        private static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.DescribeAllParametersInCamelCase();

                opt.SwaggerDoc(SwaggerDocVersion, new OpenApiInfo()
                {
                    Title = AssemblyName,
                    Version = SwaggerDocVersion
                });

                string apiXmlFile = $"{AssemblyName}.xml";
                string apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                opt.IncludeXmlComments(apiXmlPath);

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseProxyPathBase(env);

            app.UseSwagger();

            if (!env.IsProduction())
            {
                app.UseSwaggerUI(opt =>
                {
                    string url = $"./swagger/{SwaggerDocVersion}/swagger.json";

                    string name = $"{AssemblyName} - {SwaggerDocVersion}";

                    opt.SwaggerEndpoint(url, name);
                    opt.RoutePrefix = string.Empty;
                });
            }

            return app;
        }

        private static IApplicationBuilder UseProxyPathBase(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.Use((context, next) =>
                {
                    if (context.Request.Headers.TryGetValue("X-Forwarded-PathBase", out StringValues pathBase))
                    {
                        context.Request.PathBase = pathBase.ToString();
                    }

                    return next();
                });
            }

            return app;
        }
    }
}
