using TypeFlow.Application.Security;
using TypeFlow.Web.Configs;
using TypeFlow.Web.Options;

namespace TypeFlow.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(AuthSettings.Name));

            builder.Services.AddTransient<ITokenManager, TokenManager>();

            StorageExtensions.ConfigureStorageWithIdentity(builder);
            AuthExtensions.AddAuth(builder);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


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
