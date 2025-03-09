using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypeFlow.Application.Services.TypingSession;
using TypeFlow.Application.Services.TypingSession.Dto;
using TypeFlow.Application.Services.User.Dto;
using TypeFlow.Infrastructure.Context;

namespace TypeFlow.Application.Services.User
{
    public class UserService(UserManager<Core.Entities.User> userManager, ITypingSessionService typingSessionService, TypeFlowDbContext context) : IUserService
    {
        private readonly UserManager<Core.Entities.User> _userManager = userManager;
        private readonly TypeFlowDbContext _context = context;
        private readonly ITypingSessionService _typingSessionService = typingSessionService;
        public async Task<FullUserData> GetFullUserData(Guid userId)
        {
            var fullUserDataQuery = from user in _context.Users
                               where user.Id == userId
                               select new FullUserData
                               {
                                   Id = user.Id,
                                   Email = user.Email!,
                                   UserName = user.UserName!,
                                   RegisteredAt = user.RegisteredAt
  
                               };

            var fullUserData = await fullUserDataQuery.AsNoTracking().FirstOrDefaultAsync();

            if (fullUserData is null) throw new ArgumentException("Invalid id provided for user", nameof(userId));

            var stats = await _typingSessionService.GetTypingSessionStatistics(userId);

            fullUserData.Statistics = stats;

            return fullUserData;
        }
    }
}
