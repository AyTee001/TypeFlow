using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TypeFlow.Core.Entities;
using TypeFlow.Application.Security;
using TypeFlow.Web.Dto;
using Microsoft.Extensions.Options;
using TypeFlow.Web.Options;

namespace TypeFlow.Web
{
    [ApiController]
    public class AuthController(UserManager<User> userManager,
        ITokenManager tokenManager,
        IOptions<AuthSettings> authSettings) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly IOptions<AuthSettings> _authSettings = authSettings;

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationData userRegisterData)
        {
            if (userRegisterData is null) return BadRequest();

            var user = new User
            {
                Email = userRegisterData.Email,
                UserName = userRegisterData.UserName,
            };
            var result = await _userManager.CreateAsync(user, userRegisterData.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var tokenPair = await _tokenManager.IssueNewTokenPair(user);
            SetRefreshTokenCookie(tokenPair.RefreshToken);

            return Ok(tokenPair.AccessToken);
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInData userSignInData)
        {
            if (userSignInData is null) return BadRequest();

            var user = await _userManager.FindByNameAsync(userSignInData.UserName);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, userSignInData.Password)))
                return Unauthorized();

            var tokenPair = await _tokenManager.IssueNewTokenPair(user);
            SetRefreshTokenCookie(tokenPair.RefreshToken);

            return Ok(tokenPair.AccessToken);
        }

        [HttpPost("/refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies[_authSettings.Value.RefreshToken.CookieName];
            if (refreshToken is null) return Unauthorized();

            var pair = await _tokenManager.RefreshToken(refreshToken);
            SetRefreshTokenCookie(pair.RefreshToken);

            return Ok(pair.AccessToken);
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> LogOut()
        {
            var refreshToken = Request.Cookies[_authSettings.Value.RefreshToken.CookieName];
            if (refreshToken is null) return Unauthorized();

            await _tokenManager.RevokeToken(refreshToken);

            return Ok();
        }

        private void SetRefreshTokenCookie(RefreshToken token)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = token.ExpiresAt,
                MaxAge = new TimeSpan(_authSettings.Value.RefreshToken.ExpiryDays, 0, 0, 0),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            };

            this.Response.Cookies.Append(_authSettings.Value.RefreshToken.CookieName, token.Token, cookieOptions);

        }
    }
}
