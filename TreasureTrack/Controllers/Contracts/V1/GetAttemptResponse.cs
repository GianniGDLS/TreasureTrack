using System;

namespace TreasureTrack.Controllers.Contracts.V1
{
    public class GetAttemptResponse
    {
        public int AttemptId { get; set; }
        public string CodeWord { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}