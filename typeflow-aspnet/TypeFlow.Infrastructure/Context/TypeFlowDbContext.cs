using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TypeFlow.Core.Entities;

namespace TypeFlow.Infrastructure.Context
{
    public class TypeFlowDbContext(DbContextOptions<TypeFlowDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<TypingChallenge> TypingChallenges { get; set; }
        public DbSet<TypingSession> TypingSessions { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var assembly = GetType().Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
