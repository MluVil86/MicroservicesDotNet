using FluentValidation;
using OrderService.BusinessLogicLayer.DTOs;

namespace OrderService.BusinessLogicLayer.Validators;

public class OrderUpdateRequestValidator: AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        RuleFor(rule => rule.OrderID).NotEmpty().WithMessage("Order ID cannot be empty");
        RuleFor(rule => rule.UserID).NotEmpty().WithMessage("User ID cannot be empty");
        RuleFor(rule => rule.OrderDate).NotEmpty().WithMessage("Unit Price cannot be empty")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the past");
        RuleFor(rule => rule.OrderItems).NotEmpty().WithMessage("Quantity cannot be empty");
                                                
    }
}
