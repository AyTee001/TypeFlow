namespace TypeFlow.Web.Options
{
    public class JwtSettings
    {
        public string Audience { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string SigningKey { get; set; } = string.Empty;

        public int ExpiryMinutes { get; set; }
    }

}
