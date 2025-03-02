using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TypeFlow.Web.Security.Dto;
using TypeFlow.Core.Entities;
using System.Security.Cryptography;
using TypeFlow.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TypeFlow.Web.Options;

namespace TypeFlow.Application.Security
{
    public class TokenManager(IOptions<AuthSettings> authSettings,
        TypeFlowDbContext context,
        IHttpContextAccessor httpContextAccessor) : ITokenManager
    {
        private readonly IOptions<AuthSettings> _authSettings = authSettings;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly TypeFlowDbContext _context = context;

        public async Task<TokenPair> IssueNewTokenPair(User user)
        {
            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            var refreshTokenStore = new RefreshToken
            {
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_authSettings.Value.RefreshToken.ExpiryDays),
                Token = refreshToken,
                Id = Guid.NewGuid(),
                Revoked = false,
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshTokenStore);
            await _context.SaveChangesAsync();

            var pair = new TokenPair
            {
                AccessToken = new AccessToken
                {
                    Token = accessToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_authSettings.Value.Jwt.ExpiryMinutes)
                },
                RefreshToken = refreshTokenStore,
            };


            return pair;
        }

        public async Task<TokenPair> RefreshToken(string refreshToken)
        {
            var refreshTokenData = await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if(refreshTokenData is null || refreshTokenData.ExpiresAt < DateTime.UtcNow || refreshTokenData.Revoked)
            {
                throw new InvalidOperationException("Invalid refresh token!");
            }

            var newAccessToken = GenerateJwtToken(refreshTokenData.User);
            var newRefreshToken = GenerateRefreshToken();

            refreshTokenData.Token = newRefreshToken;
            refreshTokenData.ExpiresAt = DateTime.UtcNow.AddDays(_authSettings.Value.RefreshToken.ExpiryDays);

            await _context.SaveChangesAsync();

            return new TokenPair
            {
                AccessToken = new AccessToken
                {
                    Token = newAccessToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_authSettings.Value.Jwt.ExpiryMinutes)
                },
                RefreshToken = refreshTokenData,
            };
        }

        public async Task RevokeTokens(Guid userId)
        {
            if(userId != GetCurrentUserId())
            {
                throw new InvalidOperationException("Invalid user ID!");
            }

            await _context.RefreshTokens.Where(x => x.UserId == userId && !x.Revoked)
                .ExecuteUpdateAsync(x => x.SetProperty(t => t.Revoked, true));
        }

        public async Task RevokeToken(string refreshToken)
        {
            var refreshTokenData = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (refreshTokenData is null || refreshTokenData.UserId != GetCurrentUserId())
            {
                throw new InvalidOperationException("Invalid token!");
            }

            refreshTokenData.Revoked = true;
            await _context.SaveChangesAsync();
        }

        private Guid? GetCurrentUserId()
        {
            var idClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(idClaim, out var result) ? result : null;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _authSettings.Value.Jwt;

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
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[_authSettings.Value.RefreshToken.Length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}
