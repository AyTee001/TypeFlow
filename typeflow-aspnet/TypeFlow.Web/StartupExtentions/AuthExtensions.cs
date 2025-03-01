using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TypeFlow.Web.Options;

namespace TypeFlow.Web.Configs
{
    public static class AuthExtensions
    {
        public static void AddAuth(this WebApplicationBuilder builder)
        {

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                var authSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AuthSettings>>().Value;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authSettings.Jwt.Issuer,
                    ValidAudience = authSettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Jwt.SigningKey)),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
            });

            builder.Services.AddAuthorization();
        }
    }
}
