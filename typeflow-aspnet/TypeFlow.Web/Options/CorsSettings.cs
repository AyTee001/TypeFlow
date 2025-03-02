namespace TypeFlow.Web.Options
{
    public class CorsSettings
    {
        public const string Name = "Cors";

        public string[] AllowedOrigins { get; set; } = [];
        public string PolicyName { get; set; } = string.Empty;
    }
}
