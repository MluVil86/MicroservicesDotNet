using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsService.DataAccessLayer.Context;
using ProductsService.DataAccessLayer.Repository;
using ProductsService.DataAccessLayer.RepositoryContracts;

namespace ProductsService.DataAccessLayer;

public static class DependancyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddDbContext<ProductDbContext>(options =>
        {
            options.UseMySQL(configuration["ConnectionStrings:MySQLDataConnection"]!);
        });

        services.AddScoped<IProductRepository, ProductsRepository>();

        return services;
    }
}
