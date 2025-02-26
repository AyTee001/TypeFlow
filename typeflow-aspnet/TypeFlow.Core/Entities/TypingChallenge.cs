using TypeFlow.Core.Base;
using TypeFlow.Core.Enums;

namespace TypeFlow.Core.Entities
{
    class TypingChallenge : AuditableEntity
    {
        public string Text { get; set; } = string.Empty;
        public ChallengeDifficulty Difficulty { get; set; } = ChallengeDifficulty.Unset;
    }
}
