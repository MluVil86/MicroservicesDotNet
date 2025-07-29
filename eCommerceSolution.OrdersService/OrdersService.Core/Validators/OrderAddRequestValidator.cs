using FluentValidation;
using OrderService.BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.Validators;

public class OrderAddRequestValidator :AbstractValidator<OrderAddRequest>
{
}
