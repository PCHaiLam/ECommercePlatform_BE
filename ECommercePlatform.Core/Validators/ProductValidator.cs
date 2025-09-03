using ECommercePlatform.Core.Entities;
using FluentValidation;

namespace ECommercePlatform.Core.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(255).WithMessage("Product name must be at most 255 characters");

            RuleFor(p => p.Slug)
                .NotEmpty().WithMessage("Slug is required")
                .MaximumLength(255).WithMessage("Slug must be at most 255 characters")
                .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must contain lowercase letters, numbers and hyphens only");

            RuleFor(p => p.ShortDescription)
                .MaximumLength(500).WithMessage("Short description must be at most 500 characters");

            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("SKU is required")
                .MaximumLength(100).WithMessage("SKU must be at most 100 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThan(99999999.99m).WithMessage("Price must be less than 99,999,999.99");

            RuleFor(p => p.SalePrice)
                .GreaterThan(0).WithMessage("Sale price must be greater than 0")
                .LessThan(99999999.99m).WithMessage("Sale price must be less than 99,999,999.99")
                .LessThan(p => p.Price).When(p => p.SalePrice.HasValue && p.Price > 0)
                .WithMessage("Sale price must be less than price");

            RuleFor(p => p.CostPrice)
                .GreaterThan(0).WithMessage("Cost price must be greater than 0")
                .LessThan(99999999.99m).WithMessage("Cost price must be less than 99,999,999.99");

            RuleFor(p => p.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0")
                .LessThan(999999.99m).WithMessage("Weight must be less than 999,999.99");

            RuleFor(p => p.Dimensions)
                .MaximumLength(100).WithMessage("Dimensions must be at most 100 characters");

            RuleFor(p => p.Status)
                .Must(s => s == "active" || s == "inactive" || s == "draft" || s == "out_of_stock")
                .WithMessage("Status must be 'active', 'inactive', 'draft', or 'out_of_stock'");

            RuleFor(p => p.MetaTitle)
                .MaximumLength(255).WithMessage("Meta title must be at most 255 characters");

            RuleFor(p => p.MetaDescription)
                .MaximumLength(500).WithMessage("Meta description must be at most 500 characters");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Category is required");

            RuleFor(p => p.BrandId)
                .GreaterThan(0).When(p => p.BrandId.HasValue)
                .WithMessage("Invalid brand");
        }
    }
}
