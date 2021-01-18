using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using TreasureTrack.Business.Entities;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Business.Helpers.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        private readonly IUserManager _userManager;

        public CreateUserDtoValidator(IUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email).EmailAddress().WithMessage(x => "the provided email is not a valid email address");
            RuleFor(x => x.Email).MustAsync(EmailInUseAsync).WithMessage("email already in use");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("first name cannot be longer than 50 characters");
            RuleFor(x => x.LastName).MaximumLength(100).WithMessage("last name cannot be longer than 100 characters");
        }
        private async Task<bool> EmailInUseAsync(string email, CancellationToken cancellationToken)
        {
            return !await _userManager.EmailInUseAsync(email);
        }
    }
}
