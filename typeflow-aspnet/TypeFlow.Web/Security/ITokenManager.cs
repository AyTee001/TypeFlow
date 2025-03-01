using TypeFlow.Web.Security.Dto;
using TypeFlow.Core.Entities;

namespace TypeFlow.Application.Security
{
    internal interface ITokenManager
    {
        public Task<TokenPair> IssueNewTokenPair(User user);
        public Task RevokeTokens(Guid userId);
        public Task RevokeToken(string refreshToken);
        public Task<TokenPair> RefreshToken(string refreshToken);

    }
}
