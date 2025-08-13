using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using OrderService.BusinessLogicLayer.Validators;
using OrderService.BusinessLogicLayer.Mappers;
using AutoMapper;
using OrderService.BusinessLogicLayer.ServiceContracts;
using OrderService.BusinessLogicLayer.Services;
using OrderService.BusinessLogicLayer.Policies;
using OrderService.BusinessLogicLayer.PolicyContracts;


namespace OrderService.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
        services.AddAutoMapper(typeof(OrderToOrderResponseMappingProfile).Assembly);
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IOrdersValidator, OrdersValidator>();
        services.AddTransient<IPollyPolicies, PollyPolicies>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_HOST")}:" +
                                    $"{Environment.GetEnvironmentVariable("REDIS_PORT")}";
        });
        
        return services;
    }
}
