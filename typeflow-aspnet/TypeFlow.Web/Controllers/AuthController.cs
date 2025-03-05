using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TypeFlow.Core.Entities;
using TypeFlow.Application.Security;
using TypeFlow.Web.Dto;
using Microsoft.Extensions.Options;
using TypeFlow.Web.Options;

namespace TypeFlow.Web.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(UserManager<User> userManager,
        ITokenManager tokenManager,
        IOptions<AuthSettings> authSettings) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly IOptions<AuthSettings> _authSettings = authSettings;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationData userRegisterData)
        {
            if (userRegisterData is null) return BadRequest();

            var user = new User
            {
                Email = userRegisterData.Email,
                UserName = userRegisterData.UserName,
                RegisteredAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, userRegisterData.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var tokenPair = await _tokenManager.IssueNewTokenPair(user);

            var response = new TokensResponse
            {
                AccessToken = tokenPair.AccessToken.Token,
                RefreshToken = tokenPair.RefreshToken.Token,
                AccessTokenExpiration = tokenPair.AccessToken.ExpiresAt,
                RefreshTokenExpiration = tokenPair.RefreshToken.ExpiresAt
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserSignInData userSignInData)
        {
            if (userSignInData is null) return BadRequest();

            var user = await _userManager.FindByNameAsync(userSignInData.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userSignInData.Password))
                return Unauthorized();

            var tokenPair = await _tokenManager.IssueNewTokenPair(user);

            var response = new TokensResponse
            {
                AccessToken = tokenPair.AccessToken.Token,
                RefreshToken = tokenPair.RefreshToken.Token,
                AccessTokenExpiration = tokenPair.AccessToken.ExpiresAt,
                RefreshTokenExpiration = tokenPair.RefreshToken.ExpiresAt
            };

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            if (refreshTokenRequest?.RefreshToken is null) return Unauthorized();

            var tokenPair = await _tokenManager.RefreshToken(refreshTokenRequest.RefreshToken);

            var response = new TokensResponse
            {
                AccessToken = tokenPair.AccessToken.Token,
                RefreshToken = tokenPair.RefreshToken.Token,
                AccessTokenExpiration = tokenPair.AccessToken.ExpiresAt,
                RefreshTokenExpiration = tokenPair.RefreshToken.ExpiresAt
            };

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var refreshToken = Request.Cookies[_authSettings.Value.RefreshToken.CookieName];
            if (refreshToken is null) return Unauthorized();

            await _tokenManager.RevokeToken(refreshToken);

            return Ok();
        }
    }
}
