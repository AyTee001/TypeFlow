using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeFlow.Application.Services.TypingSession.Dto
{
    public class TypingSessionResultData
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ChallengeId { get; set; }
        public int FinishedInSeconds { get; set; }
        public int Errors { get; set; }
        public int CharactersCount { get; set; }
        public float Accuracy { get; set; }
        public int WordsPerMinute { get; set; }
        public int CharactersPerMinute { get; set; }
    }
}
