using ECommercePlatform.Core.Entities;
using FluentValidation;

namespace ECommercePlatform.Core.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(255).WithMessage("Category name must be at most 255 characters");

            RuleFor(c => c.Slug)
                .NotEmpty().WithMessage("Slug is required")
                .MaximumLength(255).WithMessage("Slug must be at most 255 characters")
                .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must contain lowercase letters, numbers and hyphens only");

            RuleFor(c => c.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be greater than or equal to 0");

            RuleFor(c => c.MetaTitle)
                .MaximumLength(255).WithMessage("Meta title must be at most 255 characters");

            RuleFor(c => c.MetaDescription)
                .MaximumLength(500).WithMessage("Meta description must be at most 500 characters");

            RuleFor(c => c.ImageUrl)
                .MaximumLength(500).WithMessage("Image URL must be at most 500 characters")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(c => !string.IsNullOrEmpty(c.ImageUrl))
                .WithMessage("Invalid image URL");

            RuleFor(c => c.ParentId)
                .GreaterThan(0).When(c => c.ParentId.HasValue)
                .WithMessage("Invalid parent category")
                .NotEqual(c => c.Id).When(c => c.ParentId.HasValue)
                .WithMessage("Category cannot be its own parent");
        }
    }
}
