using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductsService.BusinessLogicLayer.Mapper;
using ProductsService.BusinessLogicLayer.RabbitMQ;
using ProductsService.BusinessLogicLayer.ServiceContracts;
using ProductsService.BusinessLogicLayer.Services;
using ProductsService.BusinessLogicLayer.Validation;

namespace ProductsService.BusinessLogicLayer;

public static class DependancyInjection
{
    public static IServiceCollection AddDataBusinessLayer(this IServiceCollection services)
    {
        //Adding the assemby of one of the automapper types allows the dependency injection to all other validators of a similar type
        //only one validaor needs to be added and will cover all the others created.
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        //add assembly from one will add add all the others.
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

        services.AddScoped<IProductService, ProductService>();
        services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>(); 

        return services;
    }
}
