using TypeFlow.Core.Base;

namespace TypeFlow.Core.Entities
{
    public class TypingSession : Entity
    {
        public Guid UserId { get; set; }
        public Guid? ChallengeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int WordsTyped { get; set; }
        public int Errors { get; set; }
        public int TotalCharactersTyped { get; set; }
    }
}
