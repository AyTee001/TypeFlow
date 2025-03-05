using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeFlow.Application.Services.User.Dto;
using TypeFlow.Core.Entities;
using TypeFlow.Web.Extensions;

namespace TypeFlow.Web.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [Route("getUser")]
        public async Task<IActionResult> GetUser()
        {
            var userId = _httpContextAccessor.GetCurrentUserId();

            if (userId is null) return Unauthorized();

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == (Guid)userId);

            if (user is null) return Unauthorized();

            var userData = new UserData
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Id = user.Id,
                RegisteredAt = user.RegisteredAt
            };
            return Ok(userData);
        }
    }
}
