using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypeFlow.Core.Entities;
using TypeFlow.Infrastructure.Context;

namespace TypeFlow.Web.Configs
{
    public static class StorageConfigurations
    {
        public static void ConfigureStorageWithIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TypeFlowDbContext>(opt
                => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDataProtection();

            builder.Services
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<TypeFlowDbContext>()
                .AddApiEndpoints()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequiredLength = 12;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
        }
    }
}
