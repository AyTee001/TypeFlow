namespace TypeFlow.Web.Options
{
    public class AuthSettings
    {
        public const string Name = "Auth";
        public JwtSettings Jwt { get; set; } = new();
        public RefreshTokenSettings RefreshToken { get; set; } = new();
    }

}
