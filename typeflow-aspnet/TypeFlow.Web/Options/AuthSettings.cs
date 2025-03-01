namespace TypeFlow.Web.Options
{
    internal class AuthSettings
    {
        public const string Name = "Auth";
        public JwtSettings Jwt { get; set; } = new();
        public RefreshTokenSettings RefreshToken { get; set; } = new();
    }

}
