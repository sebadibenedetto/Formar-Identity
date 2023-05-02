using MediatR;

using Identity.Api.Swagger;
using Identity.Domain;
using Identity.Sdk.Lib;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            

            // Add services to the container.
            builder.Services.AddMediatR();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.InitializeEncryption(builder.Configuration);
            builder.Services.AddIdentity(builder.Configuration);

            builder.Services.AddSwagger();
            builder.Services.AddCors();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddDomainServices();

            var app = builder.Build();

            app.UseSwagger(app.Environment);

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}