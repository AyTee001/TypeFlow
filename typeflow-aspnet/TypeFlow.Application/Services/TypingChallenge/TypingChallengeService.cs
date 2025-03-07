using Microsoft.EntityFrameworkCore;
using TypeFlow.Application.Services.TypingChallenge.Dto;
using TypeFlow.Infrastructure.Context;

namespace TypeFlow.Application.Services.TypingChallenge
{
    public class TypingChallengeService(TypeFlowDbContext context) : ITypingChallengeService
    {
        private readonly TypeFlowDbContext _context = context;

        public async Task<TypingChallengeData> GetRandomTypingChallenge()
        {
            var typingChallenge = await _context.TypingChallenges
                .FromSqlRaw("SELECT TOP 1 * FROM TypingChallenges ORDER BY NEWID()")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (typingChallenge is null) throw new Exception("No challenges available");

            return new TypingChallengeData
            {
                Id = typingChallenge.Id,
                Text = typingChallenge.Text
            };
        }
    }
}
