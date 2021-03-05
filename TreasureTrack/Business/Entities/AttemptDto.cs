using System;

namespace TreasureTrack.Business.Entities
{
    public class AttemptDto
    {
        public int AttemptId { get; set; }
        public string CodeWord { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}