using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TypeFlow.Core.Entities;

namespace TypeFlow.Web
{
    public class JwtSettings
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
    }

    public class UserRegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserSignInDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


    [ApiController]
    public class AuthController(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterData)
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

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInDto userSignInData)
        {
            if (userSignInData is null) return BadRequest();

            var user = await _userManager.FindByNameAsync(userSignInData.UserName);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, userSignInData.Password)))
                return Unauthorized();

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }


        [HttpPost("/logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()
                ?? throw new InvalidOperationException();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Iss, jwtSettings.Issuer),
            };


            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Route("/sth")]
        [Authorize]
        public IActionResult GetSth()
        {
            return Ok("Your token works!!!!!!");
        }
    }
}
