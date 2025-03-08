using TypeFlow.Application.Services.TypingSession.Dto;
using TypeFlow.Infrastructure.Context;

namespace TypeFlow.Application.Services.TypingSession
{
    public class TypingSessionService(TypeFlowDbContext context) : ITypingSessionService
    {
        private readonly TypeFlowDbContext _context = context;

        public async Task<TypingSessionResultData> RecordTypingSession(TypingSessionData data, Guid userId)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.CharacterCount <= 0 || data.FinishedInSeconds <= 0)
            {
                throw new ArgumentException("Invalid session data");
            }

            float minutes = data.FinishedInSeconds / 60.0f;

            var accuracy = ((float)(data.CharacterCount - data.ErrorCount) / data.CharacterCount) * 100;
            var wpm = (int)(data.CharacterCount / (5.0 * minutes));
            var cpm = (int)(data.CharacterCount / minutes);

            var newTypingSession = new Core.Entities.TypingSession()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CharactersCount = data.CharacterCount,
                ChallengeId = data.ChallengeId,
                Errors = data.ErrorCount,
                FinishedInSeconds = data.FinishedInSeconds,
                Accuracy = (float)Math.Round(accuracy, 2),
                WordsPerMinute = wpm,
                CharactersPerMinute = cpm
            };

            await _context.AddRangeAsync(newTypingSession);

            await _context.SaveChangesAsync();

            var res = new TypingSessionResultData
            {
                Id = newTypingSession.Id,
                Errors = newTypingSession.Errors,
                Accuracy = newTypingSession.Accuracy,
                ChallengeId = newTypingSession.ChallengeId,
                CharactersCount = newTypingSession.CharactersCount,
                CharactersPerMinute = newTypingSession.CharactersPerMinute,
                FinishedInSeconds = newTypingSession.FinishedInSeconds,
                UserId = newTypingSession.UserId,
                WordsPerMinute = newTypingSession.WordsPerMinute
            };

            return res;
        }
    }
}
