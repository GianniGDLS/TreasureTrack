using System;

namespace TreasureTrack.Data.Entities
{
    public class Log
    {
        public Log(string info, int? userId = null)
        {
            Info = info;
            UserId = userId;
            TimeStamp = DateTime.Now;
        }

        public int LogId { get; set; }
        public string Info { get; set; }
        public int? UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}