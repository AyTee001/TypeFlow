namespace TypeFlow.Application.Services.User.Dto
{
    public class FullUserData : UserData
    {
        public float Accuracy { get; set; }
        public long TotalTests { get; set; }
        public int AverageCPM { get; set; }
        public int AverageWPM { get; set; }
    }
}
