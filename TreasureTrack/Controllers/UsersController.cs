using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TreasureTrack.Business.Entities;
using TreasureTrack.Business.Workflows.Interfaces;
using TreasureTrack.Controllers.Contracts.V1;

namespace TreasureTrack.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserWorkflow _userWorkflow;

        public UsersController(IMapper mapper, IUserWorkflow userWorkflow)
        {
            _mapper = mapper;
            _userWorkflow = userWorkflow;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest request)
        {
            var result = await _userWorkflow.CreateUserAsync(_mapper.Map<CreateUserDto>(request));
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {
            var result = await _userWorkflow.UpdateUserAsync(_mapper.Map<UpdateUserDto>(request));
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int userId, string secrectCode)
        {
            if (secrectCode == "Test123")
            {
                await _userWorkflow.DeleteUserAsync(userId);
                return NoContent();
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var result = await _userWorkflow.GetUserAsync(id);
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [Authorize]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            await _userWorkflow.ResetPasswordAsync(request.UserId, request.Password);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await _userWorkflow.LoginAsync(request.Email, request.Password);
            if (result is null)
                return NotFound();

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Email),
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                    IsPersistent = true
                });
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetAccountAsync()
        {
            var user = await _userWorkflow.GetUserAsync(HttpContext.User.Identity.Name);
            if (user is null)
                return NotFound();
            return Ok(_mapper.Map<GetUserResponse>(user));
        }

        [HttpPost("submit-attempt")]
        public async Task<IActionResult> SubmitAttemptAsync(int userId, int stageId, string codeWord)
        {
            var result = await _userWorkflow.SubmitAttemptAsync(userId, stageId, codeWord, false);
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [HttpPost("submit-child-attempt")]
        public async Task<IActionResult> SubmitChildAttemptAsync(int userId, int stageId, string codeWord)
        {
            var result = await _userWorkflow.SubmitAttemptAsync(userId, stageId, codeWord, true);
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateUserAsync(int userId)
        {
            var result = await _userWorkflow.DeactivateUserAsync(userId);
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [HttpPost("remove-test-data")]
        public async Task<IActionResult> RemoveTestDataFromUserAsync([FromQuery] int userId)
        {
            var result = await _userWorkflow.RemoveTestDataFromUserAsync(userId);
            return Ok(_mapper.Map<GetUserResponse>(result));
        }

        [HttpGet("disabled")]
        public async Task<IActionResult> GetDisabledUsersAsync()
        {
            var result = await _userWorkflow.GetDisabledUsersAsync();
            return Ok(_mapper.Map<List<GetUserResponse>>(result));
        }

        [HttpGet("stages/guessed-users")]
        public async Task<IActionResult> GetGuessedStageUsersAsync(string stageName)
        {
            var result = await _userWorkflow.GetGuessedStageUsersAsync(stageName);
            var resultWithCount = new Dictionary<int, List<GetUserResponse>>
            {
                { result.Count, _mapper.Map<List<GetUserResponse>>(result) }
            };
            return Ok(resultWithCount);
        }
    }
}