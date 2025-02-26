namespace TypeFlow.Core.Base
{
    class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; set; }
        public long CreatorId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdatedById { get; set; }
    }
}
