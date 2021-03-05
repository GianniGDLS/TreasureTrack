using System.Collections.Generic;

namespace TreasureTrack.Controllers.Contracts.V1
{
    public class GetStageResponse
    {
        public int StageId { get; set; }
        public string Name { get; set; }
        public string ChildCodeWord { get; set; }
        public bool CodeWordGuessed { get; set; }

        public List<GetAttemptResponse> Attempts { get; set; }
    }
}