using TypeFlow.Core.Base;

namespace TypeFlow.Core.Entities
{
    public class UserStatistics : Entity
    {
        public Guid UserId { get; set; }
        public float Accuracy { get; set; }
        public long TotalTests { get; set; }
        public int AverageWordsPerMinute { get; set; }
        public int AverageCharactersPerMinute { get; set; }
    }
}
