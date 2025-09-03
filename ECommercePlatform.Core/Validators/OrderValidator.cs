using ECommercePlatform.Core.Entities;
using FluentValidation;

namespace ECommercePlatform.Core.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderNumber)
                .NotEmpty().WithMessage("Order number is required")
                .MaximumLength(50).WithMessage("Order number must be at most 50 characters");

            RuleFor(o => o.Status)
                .Must(s => s == "pending" || s == "processing" || s == "shipped" || s == "delivered" || s == "cancelled" || s == "refunded")
                .WithMessage("Invalid order status");

            RuleFor(o => o.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be 3 characters")
                .Matches(@"^[A-Z]{3}$").WithMessage("Currency must be an ISO 4217 code (3 uppercase letters)");

            RuleFor(o => o.Subtotal)
                .GreaterThan(0).WithMessage("Subtotal must be greater than 0")
                .LessThan(99999999.99m).WithMessage("Subtotal must be less than 99,999,999.99");

            RuleFor(o => o.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tax must be greater than or equal to 0")
                .LessThan(99999999.99m).WithMessage("Tax must be less than 99,999,999.99");

            RuleFor(o => o.ShippingAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Shipping must be greater than or equal to 0")
                .LessThan(99999999.99m).WithMessage("Shipping must be less than 99,999,999.99");

            RuleFor(o => o.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be greater than or equal to 0")
                .LessThan(99999999.99m).WithMessage("Discount must be less than 99,999,999.99");

            RuleFor(o => o.TotalAmount)
                .GreaterThan(0).WithMessage("Total must be greater than 0")
                .LessThan(99999999.99m).WithMessage("Total must be less than 99,999,999.99");

            // Billing Information
            RuleFor(o => o.BillingFirstName)
                .NotEmpty().WithMessage("Billing first name is required")
                .MaximumLength(100).WithMessage("Billing first name must be at most 100 characters");

            RuleFor(o => o.BillingLastName)
                .NotEmpty().WithMessage("Billing last name is required")
                .MaximumLength(100).WithMessage("Billing last name must be at most 100 characters");

            RuleFor(o => o.BillingEmail)
                .NotEmpty().WithMessage("Billing email is required")
                .EmailAddress().WithMessage("Invalid billing email")
                .MaximumLength(255).WithMessage("Billing email must be at most 255 characters");

            RuleFor(o => o.BillingPhone)
                .MaximumLength(20).WithMessage("Billing phone must be at most 20 characters");

            RuleFor(o => o.BillingAddressLine1)
                .NotEmpty().WithMessage("Billing address line 1 is required")
                .MaximumLength(255).WithMessage("Billing address line 1 must be at most 255 characters");

            RuleFor(o => o.BillingCity)
                .NotEmpty().WithMessage("Billing city is required")
                .MaximumLength(100).WithMessage("Billing city must be at most 100 characters");

            RuleFor(o => o.BillingStateProvince)
                .NotEmpty().WithMessage("Billing state/province is required")
                .MaximumLength(100).WithMessage("Billing state/province must be at most 100 characters");

            RuleFor(o => o.BillingPostalCode)
                .NotEmpty().WithMessage("Billing postal code is required")
                .MaximumLength(20).WithMessage("Billing postal code must be at most 20 characters");

            RuleFor(o => o.BillingCountry)
                .NotEmpty().WithMessage("Billing country is required")
                .MaximumLength(100).WithMessage("Billing country must be at most 100 characters");

            // Shipping Information (optional)
            RuleFor(o => o.ShippingFirstName)
                .MaximumLength(100).WithMessage("Shipping first name must be at most 100 characters");

            RuleFor(o => o.ShippingLastName)
                .MaximumLength(100).WithMessage("Shipping last name must be at most 100 characters");

            RuleFor(o => o.ShippingAddressLine1)
                .MaximumLength(255).WithMessage("Shipping address line 1 must be at most 255 characters");

            RuleFor(o => o.ShippingCity)
                .MaximumLength(100).WithMessage("Shipping city must be at most 100 characters");

            RuleFor(o => o.ShippingStateProvince)
                .MaximumLength(100).WithMessage("Shipping state/province must be at most 100 characters");

            RuleFor(o => o.ShippingPostalCode)
                .MaximumLength(20).WithMessage("Shipping postal code must be at most 20 characters");

            RuleFor(o => o.ShippingCountry)
                .MaximumLength(100).WithMessage("Shipping country must be at most 100 characters");

            RuleFor(o => o.ShippingMethod)
                .MaximumLength(100).WithMessage("Shipping method must be at most 100 characters");

            RuleFor(o => o.CancellationReason)
                .MaximumLength(500).WithMessage("Cancellation reason must be at most 500 characters");

            // Date validations
            RuleFor(o => o.ShippedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Shipped date cannot be in the future");

            RuleFor(o => o.DeliveredAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Delivered date cannot be in the future")
                .GreaterThanOrEqualTo(o => o.ShippedAt).When(o => o.ShippedAt.HasValue)
                .WithMessage("Delivered date must be after shipped date");

            RuleFor(o => o.CancelledAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Cancelled date cannot be in the future");
        }
    }
}
