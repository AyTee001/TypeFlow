using System.Security.Claims;

namespace TypeFlow.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid? GetCurrentUserId(this HttpContext context)
        {
            var idClaim = context?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(idClaim, out var result) ? result : null;
        }

    }
}
