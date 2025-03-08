using Microsoft.AspNetCore.Mvc;
using TypeFlow.Application.Services.TypingSession;
using TypeFlow.Application.Services.TypingSession.Dto;
using TypeFlow.Web.Extensions;

namespace TypeFlow.Web.Controllers
{
    [Route("typingSession")]
    [ApiController]
    public class TypingSessionController(ITypingSessionService typingSessionService) : ControllerBase
    {
        private readonly ITypingSessionService _typingSessionService = typingSessionService;

        [HttpPost("recordSession")]
        public async Task<IActionResult> RecordTypingSession(TypingSessionData typingSession)
        {
            var userId = HttpContext.GetCurrentUserId();

            if (userId is null) return Unauthorized();

            var typingSessionResult = await _typingSessionService.RecordTypingSession(typingSession, (Guid)userId);

            return Ok(typingSessionResult);
        }
    }
}
