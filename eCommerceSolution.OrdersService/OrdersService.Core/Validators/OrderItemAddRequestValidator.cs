using FluentValidation;
using OrderService.BusinessLogicLayer.DTOs;

namespace OrderService.BusinessLogicLayer.Validators;

public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
{
    public OrderItemAddRequestValidator()
    {
        RuleFor(rule => rule.ProductID).NotEmpty().WithMessage("Product ID cannot be empty");
        RuleFor(rule => rule.UnitPrice).NotEmpty().WithMessage("Unit Price cannot be empty");
        RuleFor(rule => rule.Quantity).NotEmpty().WithMessage("Quantity cannot be empty")
                                      .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}
