using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypeFlow.Core.Entities;

namespace TypeFlow.Infrastructure.Context.Config
{
    public class TypingSessionConfiguration : IEntityTypeConfiguration<TypingSession>
    {
        public void Configure(EntityTypeBuilder<TypingSession> builder)
        {
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TypingChallenge>()
                .WithMany()
                .HasForeignKey(x => x.ChallengeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
