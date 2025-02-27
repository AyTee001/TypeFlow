using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeFlow.Core.Entities;

namespace TypeFlow.Infrastructure.Context.Config
{
    public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
    {
        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<UserStatistics>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
