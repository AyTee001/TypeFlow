using TypeFlow.Core.Base;

namespace TypeFlow.Core.Entities
{
    public class TypingChallenge : AuditableEntity
    {
        public string Text { get; set; } = string.Empty;
    }
}
