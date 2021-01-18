using AutoMapper;
using FluentValidation;
using Mollie.Api.Client;
using Mollie.Api.Models;
using Mollie.Api.Models.Payment;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using System;
using System.Threading.Tasks;
using TreasureTrack.Business.Entities;
using TreasureTrack.Business.Helpers;
using TreasureTrack.Business.Helpers.Validators;
using TreasureTrack.Business.Workflows.Interfaces;
using TreasureTrack.Data.Entities;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Business.Workflows
{
    public class UserWorkflow : IUserWorkflow
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ILogManager _logManager;

        public UserWorkflow(IMapper mapper, IUserManager userManager, ILogManager logManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logManager = logManager;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            var validator = new CreateUserDtoValidator(_userManager);
            User result;
            await validator.ValidateAndThrowAsync(user);
            try
            {
                result = await _userManager.CreateUserAsync(_mapper.Map<User>(user));
            }
            catch (Exception e)
            {
                await _logManager.WriteLogAsync(e.Message);
                throw;
            }
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto user)
        {
            User result;
            try
            {
                result = await _userManager.UpdateUserAsync(_mapper.Map<User>(user));
            }
            catch (Exception e)
            {
                await _logManager.WriteLogAsync(e.Message, user.UserId);
                throw;
            }
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
            if (PasswordHash.ValidatePassword(password, user.Password))
                throw new Exception("password cannot be the same as your last password");
            try
            {
                await _userManager.ResetPasswordAsync(userId, PasswordHash.CreateHash(password));
            }
            catch (Exception e)
            {
                await _logManager.WriteLogAsync(e.Message, userId);
                throw;
            }
        }

        public async Task<UserDto> LoginAsync(string email, string password)
        {
            var user = await _userManager.GetUserAsync(email);
            if (user is null)
                return null;
            if (PasswordHash.ValidatePassword(password, user.Password))
                return _mapper.Map<UserDto>(user);
            throw new Exception("incorrect password provided");
        }

       
    }
}