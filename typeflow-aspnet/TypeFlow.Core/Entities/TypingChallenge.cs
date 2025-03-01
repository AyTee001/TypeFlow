using TypeFlow.Core.Base;
using TypeFlow.Core.Entities.Enums;

namespace TypeFlow.Core.Entities
{
    public class TypingChallenge : AuditableEntity
    {
        public string Text { get; set; } = string.Empty;
        public ChallengeDifficulty Difficulty { get; set; } = ChallengeDifficulty.Unset;
    }
}
