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
        public async Task<IActionResult> RecordTypingSession([FromBody]TypingSessionData typingSession)
        {
            var userId = HttpContext.GetCurrentUserId();

            if (userId is null) return Unauthorized();

            var typingSessionResult = await _typingSessionService.RecordTypingSession(typingSession, (Guid)userId);
            return Ok(typingSessionResult);
        }

        [HttpGet("general")]
        public async Task<IActionResult> GetGeneralTypingSessionStats()
        {
            var userId = HttpContext.GetCurrentUserId();

            if (userId is null) return Unauthorized();

            var statistics = await _typingSessionService.GetTypingSessionStatistics((Guid)userId);
            return Ok(statistics);
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetTypingSessionStatisticsForChart()
        {
            var userId = HttpContext.GetCurrentUserId();

            if (userId is null) return Unauthorized();

            var statistics = await _typingSessionService.GetTypingSessionStatisticsForChart((Guid)userId);
            return Ok(statistics);
        }
    }
}
