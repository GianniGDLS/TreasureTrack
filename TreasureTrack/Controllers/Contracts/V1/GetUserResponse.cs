using System.Collections.Generic;

namespace TreasureTrack.Controllers.Contracts.V1
{
    public class GetUserResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool SuccessfullyPaid { get; set; }
        public int Count { get; set; }

        public List<GetStageResponse> Stages { get; set; }
    }
}