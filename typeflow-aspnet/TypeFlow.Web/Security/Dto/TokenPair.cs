using TypeFlow.Core.Entities;

namespace TypeFlow.Web.Security.Dto
{
    public class TokenPair
    {
        public required AccessToken AccessToken { get; set; }
        public required RefreshToken RefreshToken { get; set; }
    }
}
