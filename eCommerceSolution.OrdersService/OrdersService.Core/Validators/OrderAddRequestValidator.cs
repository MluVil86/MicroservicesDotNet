using FluentValidation;
using OrderService.BusinessLogicLayer.DTOs;

namespace OrderService.BusinessLogicLayer.Validators;

public class OrderAddRequestValidator :AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(rule => rule.UserID).NotEmpty().WithMessage("UserID cannot be empty");
        RuleFor(rule => rule.OrderDate).NotEmpty().WithMessage("Order Date cannot be empty")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the past"); 
        RuleFor(rule => rule.OrderItems).NotEmpty().WithMessage("Order items cannot be empty");
    }
}
