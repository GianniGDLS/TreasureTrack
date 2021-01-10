using System.Threading.Tasks;
using TreasureTrack.Business.Entities;

namespace TreasureTrack.Business.Workflows.Interfaces
{
    public interface IUserWorkflow
    {
        Task<UserDto> CreateUserAsync(CreateUserDto user);
        Task DeleteUserAsync(int userId);
        Task<UserDto> GetUserAsync(int userId);
        Task<UserDto> GetUserAsync(string userName);
        Task<UserDto> UpdateUserAsync(UpdateUserDto user);
        Task ResetPasswordAsync(int userId, string password);
        Task<UserDto> LoginAsync(string email, string password);
        Task ActivateRegistrationAsync(int userId);
    }
}