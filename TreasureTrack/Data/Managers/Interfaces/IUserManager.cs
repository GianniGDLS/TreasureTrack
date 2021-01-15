using System.Threading.Tasks;
using TreasureTrack.Data.Entities;

namespace TreasureTrack.Data.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<User> CreateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserAsync(int userId);
        Task<User> GetUserAsync(string email);
        Task<User> UpdateUserAsync(User user);
        Task ResetPasswordAsync(int userId, string hashedPassword);
        Task<bool> EmailInUseAsync(string email);
        Task ActivateRegistrationAsync(int userId);
        Task SavePaymentIdForUserAsync(int userId, string paymentId);
    }
}