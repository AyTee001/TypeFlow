using Microsoft.Extensions.Options;
using TypeFlow.Web.Options;

namespace TypeFlow.Web.StartupExtentions
{
    public static class CorsExtensions
    {
        public static void AddCors(this WebApplicationBuilder builder)
        {
            var corsSettings = builder.Configuration.GetSection(CorsSettings.Name).Get<CorsSettings>();
            if (corsSettings is null || string.IsNullOrWhiteSpace(corsSettings.PolicyName)) return;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                        name: corsSettings.PolicyName,
                        policy => policy.WithOrigins(corsSettings.AllowedOrigins ?? [])
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
            });
        }
    }
}
