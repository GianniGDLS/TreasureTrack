﻿using System.Collections.Generic;

namespace TreasureTrack.Data.Entities
{
    public class Stage
    {
        public int StageId { get; set; }
        public string Name { get; set; }
        public string CodeWord { get; set; }
        public string ChildCodeWord { get; set; }
        public bool CodeWordGuessed { get; set; }

        public List<Attempt> Attempts { get; set; }
        public User User { get; set; }
    }
}