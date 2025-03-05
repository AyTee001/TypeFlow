using Microsoft.AspNetCore.Identity;
using TypeFlow.Application.Services.User.Dto;

namespace TypeFlow.Application.Services
{
    public class UserService(UserManager<Core.Entities.User> userManager)
    {
        private readonly UserManager<Core.Entities.User> _userManager = userManager;
        public Task<FullUserData> GetFullUserData(Guid userId)
        {
            return Task.FromResult(new FullUserData());
        }
    }
}
