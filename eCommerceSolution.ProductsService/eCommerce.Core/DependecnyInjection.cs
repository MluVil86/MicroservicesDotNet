using eCommerce.Core.ServiceContracrs;
using eCommerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core;

public class DependecnyInjection
{
    public static IServiceCollection AddCore(IServiceCollection services) 
    {
        services.AddTransient<IProduct, ProductService>();

        return services;
    
    
    }
}
