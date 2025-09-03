using ECommercePlatform.Core.Entities;
using FluentValidation;

namespace ECommercePlatform.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must be at most 255 characters");

            RuleFor(u => u.PasswordHash)
                .NotEmpty().WithMessage("Password is required")
                .MaximumLength(255).WithMessage("Password must be at most 255 characters");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name must be at most 100 characters");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name must be at most 100 characters");

            RuleFor(u => u.Phone)
                .MaximumLength(20).WithMessage("Phone must be at most 20 characters")
                .Matches(@"^[\+]?[1-9][\d]{0,15}$").When(u => !string.IsNullOrEmpty(u.Phone))
                .WithMessage("Invalid phone number");

            RuleFor(u => u.Gender)
                .Must(g => string.IsNullOrEmpty(g) || g == "male" || g == "female" || g == "other")
                .WithMessage("Gender must be 'male', 'female', or 'other'");

            RuleFor(u => u.AvatarUrl)
                .MaximumLength(500).WithMessage("Avatar URL must be at most 500 characters")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(u => !string.IsNullOrEmpty(u.AvatarUrl))
                .WithMessage("Invalid avatar URL");

            RuleFor(u => u.Status)
                .Must(s => s == "active" || s == "inactive" || s == "banned")
                .WithMessage("Status must be 'active', 'inactive', or 'banned'");

            RuleFor(u => u.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past")
                .GreaterThan(DateTime.Now.AddYears(-150)).WithMessage("Invalid date of birth")
                .When(u => u.DateOfBirth.HasValue);
        }
    }
}
