using TypeFlow.Application.Services.TypingSession.Dto;

namespace TypeFlow.Application.Services.TypingSession
{
    public interface ITypingSessionService
    {
        Task<TypingSessionResultData> RecordTypingSession(TypingSessionData data, Guid userId);
    }
}
