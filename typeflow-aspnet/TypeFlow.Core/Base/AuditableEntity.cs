namespace TypeFlow.Core.Base
{
    public class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; set; }
        public long CreatorId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdatedById { get; set; }
    }
}
