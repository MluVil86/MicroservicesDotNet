using FluentValidation;
using OrderService.BusinessLogicLayer.DTOs;

namespace OrderService.BusinessLogicLayer.Validators;

public class OrderItemUpdateRequestValidator: AbstractValidator<OrderItemUpdateRequest>
{
    public OrderItemUpdateRequestValidator()
    {
        RuleFor(rule => rule.ProductID).NotEmpty().WithMessage("Product ID cannot be empty");
        RuleFor(rule => rule.UnitPrice).NotEmpty().WithMessage("Unit Price cannot be empty")
                                       .GreaterThan(-1).WithMessage("Quantity must be greater than -1"); ;
    }
}
