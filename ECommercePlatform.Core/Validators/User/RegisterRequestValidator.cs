using ECommercePlatform.Core.DTOs.User;
using FluentValidation;

namespace ECommercePlatform.Core.Validators.User
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid")
                .MaximumLength(255);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters");

            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .NotEmpty().WithMessage("Phone is required");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past")
                .NotEmpty().WithMessage("Date of birth is required");

            RuleFor(x => x.Gender)
                .Must(g => g == "male" || g == "female")
                .NotEmpty().WithMessage("Gender is required");
        }
    }
}


