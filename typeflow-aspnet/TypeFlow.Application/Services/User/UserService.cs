using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypeFlow.Application.Services.User.Dto;
using TypeFlow.Infrastructure.Context;

namespace TypeFlow.Application.Services.User
{
    public class UserService(UserManager<Core.Entities.User> userManager, TypeFlowDbContext context) : IUserService
    {
        private readonly UserManager<Core.Entities.User> _userManager = userManager;
        private readonly TypeFlowDbContext _context = context;
        public async Task<FullUserData> GetFullUserData(Guid userId)
        {
            var fullUserDataQuery = from user in _context.Users
                               join statistics in _context.UserStatistics on user.Id equals statistics.UserId into tmp
                               from userStatistics in tmp.DefaultIfEmpty()
                               where user.Id == userId
                               select new FullUserData
                               {
                                   Id = user.Id,
                                   Email = user.Email!,
                                   Accuracy = userStatistics == null ? 0 : userStatistics.Accuracy,
                                   AverageCPM = userStatistics == null ? 0 : userStatistics.AverageCharactersPerMinute,
                                   AverageWPM = userStatistics == null ? 0 : userStatistics.AverageWordsPerMinute,
                                   RegisteredAt = user.RegisteredAt,
                                   TotalTests = userStatistics == null ? 0 : userStatistics.TotalTests,
                                   UserName = user.UserName!
                               };

            var fullUserData = await fullUserDataQuery.AsNoTracking().FirstOrDefaultAsync();

            return fullUserData is null
                ? throw new ArgumentException("Invalid id provided for user", nameof(userId))
                : fullUserData;
        }
    }
}
