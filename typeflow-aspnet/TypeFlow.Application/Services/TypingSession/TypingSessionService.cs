using Microsoft.EntityFrameworkCore;
using System.Linq;
using TypeFlow.Application.Services.TypingSession.Dto;
using TypeFlow.Infrastructure.Context;

namespace TypeFlow.Application.Services.TypingSession
{
    public class TypingSessionService(TypeFlowDbContext context) : ITypingSessionService
    {
        private record DayData(DateTime Day, int? Accuracy, int? Wpm);

        private readonly TypeFlowDbContext _context = context;

        public async Task<TypingSessionResultData> RecordTypingSession(TypingSessionData data, Guid userId)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.CharacterCount <= 0 || data.FinishedInSeconds <= 0)
            {
                throw new ArgumentException("Invalid session data");
            }

            float minutes = data.FinishedInSeconds / 60.0f;

            var accuracy = ((float)(data.CharacterCount - data.ErrorCount) / data.CharacterCount);
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
                CharactersPerMinute = cpm,
                FinishedAt = DateTime.UtcNow
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

        public async Task<TypingSessionStatistics> GetTypingSessionStatistics(Guid userId)
        {
            var bestSession = await _context.TypingSessions
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.WordsPerMinute)
                .ThenByDescending(x => x.Accuracy)
                .ThenBy(x => x.Errors)
                .FirstOrDefaultAsync();

            var worstSession = await _context.TypingSessions
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.WordsPerMinute)
                .ThenBy(x => x.Accuracy)
                .ThenByDescending(x => x.Errors)
                .FirstOrDefaultAsync();

            var result = new TypingSessionStatistics
            {
                BestResult = bestSession is null ? null
                    : new TypingSessionResultData
                    {
                        Id = bestSession.Id,
                        Errors = bestSession.Errors,
                        Accuracy = bestSession.Accuracy,
                        ChallengeId = bestSession.ChallengeId,
                        CharactersCount = bestSession.CharactersCount,
                        CharactersPerMinute = bestSession.CharactersPerMinute,
                        FinishedInSeconds = bestSession.FinishedInSeconds,
                        UserId = bestSession.UserId,
                        WordsPerMinute = bestSession.WordsPerMinute
                    },
                WorstResult = worstSession is null ? null
                    : new TypingSessionResultData
                    {
                        Id = worstSession.Id,
                        Errors = worstSession.Errors,
                        Accuracy = worstSession.Accuracy,
                        ChallengeId = worstSession.ChallengeId,
                        CharactersCount = worstSession.CharactersCount,
                        CharactersPerMinute = worstSession.CharactersPerMinute,
                        FinishedInSeconds = worstSession.FinishedInSeconds,
                        UserId = worstSession.UserId,
                        WordsPerMinute = worstSession.WordsPerMinute
                    }
            };

            return result;
        }

        public async Task<TypingSessionChartStatistics> GetTypingSessionStatisticsForChart(Guid userId)
        {
            var today = DateTime.UtcNow.Date;
            var daysAgo = DateTime.UtcNow.Date.AddDays(-10);

            var dataQuery = from session in _context.TypingSessions
                                 where session.FinishedAt >= daysAgo && session.FinishedAt <= today && session.UserId == userId
                                 group session by session.FinishedAt.Date into groupByDay
                                 select new DayData
                                 (
                                     groupByDay.Key,
                                     (int)((groupByDay.Sum(x => (x.Accuracy * 100)))/groupByDay.Count()),
                                     (int)((groupByDay.Sum(x => x.WordsPerMinute)) / groupByDay.Count())
                                 );

            var data = await dataQuery.ToListAsync();

            for(var day = daysAgo; day <= today; day = day.AddDays(1))
            {
                if (data.FirstOrDefault(x => x.Day == day) is null)
                {
                    data.Add(new DayData(
                        day,
                        null,
                        null
                    ));
                }
            }

            var res = new TypingSessionChartStatistics
            {
                AccuracyValues = [],
                WpmValues = [],
                Dates = [],
            };

            data.OrderBy(x => x.Day).ToList().ForEach(x =>
            {
                res.Dates.Add(x.Day);
                res.WpmValues.Add(x.Wpm);
                res.AccuracyValues.Add(x.Accuracy);
            });

            return res;
        }
    }
}
