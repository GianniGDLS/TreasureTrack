using System;
using System.Collections.Generic;
using System.Linq;

namespace TreasureTrack.Data.Entities
{
    public class Stage
    {
        public Stage()
        {
            Attempts = new List<Attempt>();
            AttemptCount = Attempts.Count;
            Guessed = Attempts.Any(x => x.CodeWord == CodeWord);
        }

        public int PuzzleId { get; set; }
        public string PuzzleName { get; set; }
        public DateTime DateOnline { get; set; }
        public string CodeWord { get; set; }
        public int AttemptCount { get; set; }
        public bool Guessed { get; set; }

        public List<Attempt> Attempts { get; set; }
    }
}