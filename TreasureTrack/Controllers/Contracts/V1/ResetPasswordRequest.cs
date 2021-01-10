namespace TreasureTrack.Controllers.Contracts.V1
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}