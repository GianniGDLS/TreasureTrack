using System.Collections.Generic;

namespace TreasureTrack.Business.Entities
{
    public class StageDto
    {
        public int StageId { get; set; }
        public string Name { get; set; }
        public string CodeWord { get; set; }
        public string ChildCodeWord { get; set; }
        public bool CodeWordGuessed { get; set; }

        public List<AttemptDto> Attempts { get; set; }
    }
}