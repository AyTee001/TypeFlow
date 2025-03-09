using TypeFlow.Core.Base;

namespace TypeFlow.Core.Entities
{
    public class TypingSession : Entity
    {
        public Guid UserId { get; set; }
        public Guid? ChallengeId { get; set; }
        public int FinishedInSeconds { get; set; }
        public int Errors { get; set; }
        public int CharactersCount { get; set; }

        public float Accuracy { get; set; }
        public int WordsPerMinute { get; set; }
        public int CharactersPerMinute { get; set; }

        public DateTime FinishedAt { get; set; }
    }
}
