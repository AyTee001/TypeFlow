namespace TypeFlow.Web.Security.Dto
{
    public class AccessToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
