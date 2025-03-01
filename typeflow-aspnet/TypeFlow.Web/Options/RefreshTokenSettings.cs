namespace TypeFlow.Web.Options
{
    internal class RefreshTokenSettings
    {
        public int ExpiryDays { get; set; }

        public string CookieName { get; set; } = string.Empty;

        public int Length { get; set; } = 32;
    }
}
