using TypeFlow.Web.Options;

namespace TypeFlow.Web.StartupExtentions
{
    public static class OptionsExtensions
    {
        public static void AddOptionsAndConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(AuthSettings.Name));
        }
    }
}
