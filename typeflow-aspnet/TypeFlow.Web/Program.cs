using TypeFlow.Web.Configs;

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


            StorageConfigurations.ConfigureStorageWithIdentity(builder);
            AuthConfigurations.AddAuth(builder);


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
