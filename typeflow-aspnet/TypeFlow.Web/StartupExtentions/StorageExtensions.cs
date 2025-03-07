using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypeFlow.Core.Entities;
using TypeFlow.Infrastructure.Context;
using TypeFlow.Infrastructure.Seed;

namespace TypeFlow.Web.Configs
{
    public static class StorageExtensions
    {
        public static void ConfigureStorageWithIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TypeFlowDbContext>(opt
                => {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                    opt.UseSeeding((context, _) =>
                    {
                        context.Set<TypingChallenge>().ExecuteDelete();
                        var challenges = SeedingHelper.GetTypingChallenges();
                        context.Set<TypingChallenge>().AddRange(challenges);
                        context.SaveChanges();
                    });
                });

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
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
        }
    }
}
