using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using OrderService.BusinessLogicLayer.Validators;
using OrderService.BusinessLogicLayer.Mappers;
using AutoMapper;
using OrderService.BusinessLogicLayer.ServiceContracts;
using OrderService.BusinessLogicLayer.Services;


namespace OrderService.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
        services.AddAutoMapper(typeof(OrderToOrderResponseMappingProfile).Assembly);
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IOrdersValidator, OrdersValidator>();

        return services;
    }
}
