using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            user.Stages = SeedStages();
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users
                .Include(x => x.Stages)
                    .ThenInclude(x => x.Attempts)
                .FirstAsync(x => x.UserId == userId);
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

        public async Task<User> SubmitAttemptAsync(int userId, int stageId, string codeWord, bool childAttempt)
        {
            var user = await GetUserAsync(userId);
            var stage = user.Stages.First(x => x.StageId == stageId);
            if (!childAttempt)
            {
                stage.Attempts.Add(new Attempt(codeWord));
                if (string.Equals(stage.CodeWord, codeWord, System.StringComparison.InvariantCultureIgnoreCase))
                    stage.CodeWordGuessed = true;
            }

            else
                stage.ChildCodeWord = codeWord;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeactivateUserAsync(int userId)
        {
            var user = await GetUserAsync(userId);
            user.Enabled = false;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> RemoveTestDataFromUserAsync(int userId)
        {
            var user = await GetUserAsync(userId);
            foreach (var stage in user.Stages)
            {
                stage.ChildCodeWord = null;
                stage.CodeWordGuessed = false;
            }
            var attempts = user.Stages.SelectMany(x => x.Attempts);
            _context.Attempts.RemoveRange(attempts);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetDisabledUsersAsync()
        {
            var userIds = await _context.Stages
                .Where(x => x.Attempts.Count == 3 && x.CodeWordGuessed == false)
                .Select(x => x.User.UserId)
                .ToListAsync();

            return await _context.Users
                .Include(x => x.Stages)
                    .ThenInclude(x => x.Attempts)
                .Where(x => userIds.Contains(x.UserId))
                .ToListAsync();
        }

        public async Task<List<User>> GetGuessedStageUsersAsync(string stageName)
        {
            return await _context.Users
                .Include(x => x.Stages)
                    .ThenInclude(x => x.Attempts)
                .Where(x => x.Stages.First(x => x.Name == stageName).CodeWordGuessed)
                .ToListAsync();
        }

        private List<Stage> SeedStages()
        {
            return new List<Stage>
            {
                new Stage
                {
                    Name = "Etappe 1",
                    CodeWord = "BROOX"
                },
                new Stage
                {
                    Name = "Etappe 2",
                    CodeWord = "DOEL"
                },
                new Stage
                {
                    Name = "Etappe 3",
                    CodeWord = "1850"
                },
                new Stage
                {
                    Name = "Etappe 4",
                    CodeWord = "5244"
                }
            };
        }
    }
}