using Microsoft.AspNetCore.Mvc;
using TypeFlow.Application.Services.TypingChallenge;

namespace TypeFlow.Web.Controllers
{
    [Route("typingChallenge")]
    [ApiController]
    public class TypingChallengeController(ITypingChallengeService typingChallengeService) : ControllerBase
    {
        private readonly ITypingChallengeService _typingChallengeService = typingChallengeService;

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomChallenge()
        {
            var challenge = await _typingChallengeService.GetRandomTypingChallenge();
            return Ok(challenge);
        }
    }
}
