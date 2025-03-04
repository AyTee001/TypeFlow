using System.Security.Claims;

namespace TypeFlow.Web.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid? GetCurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(idClaim, out var result) ? result : null;
        }

    }
}
