using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.ServiceContracts;

public interface IOrdersValidator
{
    Task<List<string>> Validate<T>(IValidator<T> validator, List<T> orderItems);
    Task<List<string>> Validate<T>(IValidator<T> validator, T order);
}
