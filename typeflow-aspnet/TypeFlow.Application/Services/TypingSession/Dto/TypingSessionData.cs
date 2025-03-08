namespace TypeFlow.Application.Services.TypingSession.Dto
{
    public class TypingSessionData
    {
        public Guid? ChallengeId { get; set; }
        public int CharacterCount { get; set; }
        public int ErrorCount { get; set; }
        public int FinishedInSeconds { get; set; }
    }
}
