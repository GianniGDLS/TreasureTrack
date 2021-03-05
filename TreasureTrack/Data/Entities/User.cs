using System.Collections.Generic;

namespace TreasureTrack.Data.Entities
{
    public class User
    {
        public User()
        {
            SuccessfullyPaid = false;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SuccessfullyPaid { get; set; }
        public string PaymentId { get; set; }
        public bool Enabled { get; set; } = true;

        public List<Stage> Stages { get; set; }
    }
}