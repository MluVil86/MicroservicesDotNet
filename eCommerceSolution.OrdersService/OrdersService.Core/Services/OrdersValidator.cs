using FluentValidation;
using OrderService.BusinessLogicLayer.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.Services
{
    internal class OrdersValidator : IOrdersValidator
    {
        public async Task<List<string>> Validate<T>(IValidator<T> validator, List<T> orderItems)
        {
            var validationTasks = orderItems.Select(t => validator.ValidateAsync(t)).ToList();
            var validationResult = await Task.WhenAll(validationTasks);

            return validationResult.Where(r => !r.IsValid).SelectMany(r => r.Errors).Select(r => r.ErrorMessage).Distinct().ToList();
        }

        public async Task<List<string>> Validate<T>(IValidator<T> validator, T order)
        {
            var validationTasks = await validator.ValidateAsync(order);

            return validationTasks.Errors.Select(r => r.ErrorMessage).Distinct().ToList();
        }
    }
}
