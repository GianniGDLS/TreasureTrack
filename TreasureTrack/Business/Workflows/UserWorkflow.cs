using AutoMapper;
using System.Threading.Tasks;
using TreasureTrack.Business.Entities;
using TreasureTrack.Business.Helpers;
using TreasureTrack.Business.Workflows.Interfaces;
using TreasureTrack.Data.Entities;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Business.Workflows
{
    public class UserWorkflow : IUserWorkflow
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public UserWorkflow(IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            if (await _userManager.EmailInUseAsync(user.Email))
                throw new System.Exception("email already in use");
            var result = await _userManager.CreateUserAsync(_mapper.Map<User>(user));
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto user)
        {
            var result = await _userManager.UpdateUserAsync(_mapper.Map<User>(user));
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            var result = await _userManager.GetUserAsync(userId);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> GetUserAsync(string userName)
        {
            var result = await _userManager.GetUserAsync(userName);
            return _mapper.Map<UserDto>(result);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userManager.DeleteUserAsync(userId);
        }

        public async Task ResetPasswordAsync(int userId, string password)
        {
            var user = await _userManager.GetUserAsync(userId);
            if (string.Equals(user.Password, PasswordHash.CreateHash(password)))
                throw new System.Exception("password cannot be the same as your last password");
            await _userManager.ResetPasswordAsync(userId, PasswordHash.CreateHash(password));
        }

        public async Task<UserDto> LoginAsync(string email, string password)
        {
            var user = await _userManager.GetUserAsync(email);
            if (user is null)
                return null;
            if(!user.SuccessfullyPaid)
                throw new System.Exception("the user has not yet paid");
            if (PasswordHash.ValidatePassword(password, user.Password))
                return _mapper.Map<UserDto>(user);
            throw new System.Exception("incorrect password provided");
        }

        public async Task ActivateRegistrationAsync(int userId)
        {
            await _userManager.ActivateRegistrationAsync(userId);
        }
    }
}