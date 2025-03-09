using TypeFlow.Application.Services.TypingSession.Dto;

namespace TypeFlow.Application.Services.User.Dto
{
    public class FullUserData : UserData
    {
        public TypingSessionStatistics? Statistics { get; set; }
    }
}
