namespace TypeFlow.Application.Services.TypingSession.Dto
{
    public class TypingSessionChartStatistics
    {
        public List<DateTime> Dates { get; set; } = [];
        public List<int?> WpmValues { get; set; } = [];
        public List<int?> AccuracyValues { get; set; } = [];
    }

}
