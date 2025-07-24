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
        
        string connectionStringTemplate = configuration["MySQLDataConn"]!;

        string connectionString = connectionStringTemplate
                                .Replace("$SERVER", configuration["SERVER"])                                
                                .Replace("$DATABASE", configuration["DATABASE"])
                                .Replace("$PORT", configuration["PORT"])
                                .Replace("$UID", configuration["UID"])
                                .Replace("$PASSWORD", configuration["PASSWORD"]);
        
        services.AddDbContext<ProductDbContext>(options =>
        {                        
            options.UseMySQL(connectionString);
        });

        services.AddScoped<IProductRepository, ProductsRepository>();

        return services;
    }
}