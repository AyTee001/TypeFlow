using TypeFlow.Application.Services.TypingChallenge.Dto;

namespace TypeFlow.Application.Services.TypingChallenge
{
    public interface ITypingChallengeService
    {
        Task<TypingChallengeData> GetRandomTypingChallenge();
    }
}
