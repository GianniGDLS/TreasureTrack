using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TreasureTrack.Data.Entities;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Data.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ProjectDbContext _context;

        public UserManager(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await GetUserAsync(user.UserId);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPasswordAsync(int userId, string hashedPassword)
        {
            var user = await GetUserAsync(userId);
            user.Password = hashedPassword;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailInUseAsync(string email)
        {
            return await _context.Users.AnyAsync(x => string.Equals(x.Email.ToUpper(), email.ToUpper()));
        }

        public async Task SavePaymentIdForUserAsync(int userId, string paymentId)
        {
            var user = await GetUserAsync(userId);
            user.PaymentId = paymentId;
            await _context.SaveChangesAsync();
        }

        public async Task ActivateRegistrationAsync(int userId)
        {
            var user = await GetUserAsync(userId);
            user.SuccessfullyPaid = true;
            await _context.SaveChangesAsync();
        }
    }
}