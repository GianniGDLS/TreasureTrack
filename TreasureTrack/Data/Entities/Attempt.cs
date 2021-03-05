using System;

namespace TreasureTrack.Data.Entities
{
    public class Attempt
    {
        public Attempt(string codeWord)
        {
            TimeStamp = DateTime.Now;
            CodeWord = codeWord;
        }

        public int AttemptId { get; set; }
        public string CodeWord { get; set; }
        public DateTime TimeStamp { get; set; }

        public Stage Stage { get; set; }
    }
}