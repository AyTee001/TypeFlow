using Microsoft.Extensions.Options;
using TypeFlow.Application.Security;
using TypeFlow.Web.Configs;
using TypeFlow.Web.Options;
using TypeFlow.Web.StartupExtentions;

namespace TypeFlow.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddOptionsAndConfiguration();
            builder.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddTransient<ITokenManager, TokenManager>();

            builder.ConfigureStorageWithIdentity();
            builder.AddAuth();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            var corsSettings = app.Configuration.GetSection(CorsSettings.Name).Get<CorsSettings>();
            app.UseCors(corsSettings?.PolicyName ?? string.Empty);

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("hello/", () =>
            {
                return "Hello, World!";
            });

            app.Run();
        }
    }
}
