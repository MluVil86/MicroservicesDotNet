using FluentValidation;
using ProductsService.BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsService.BusinessLogicLayer.Validation;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product Name can't be blank");
        RuleFor(temp => temp.Category).IsInEnum().WithMessage("Category name is not valid");
        RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should be between 0 to {double.MaxValue}");
        RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in Stock should be between 0 to {double.MaxValue}");
    }
}