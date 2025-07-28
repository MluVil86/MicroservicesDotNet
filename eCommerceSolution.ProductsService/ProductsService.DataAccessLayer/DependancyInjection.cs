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
        string connectionStringTemplate = configuration.GetConnectionString("AppConnection")!;

        string connectionString = connectionStringTemplate
                                .Replace("$SERVER", Environment.GetEnvironmentVariable("SERVER"))
                                .Replace("$DATABASE", Environment.GetEnvironmentVariable("DATABASE"))
                                .Replace("$PORT", Environment.GetEnvironmentVariable("PORT"))
                                .Replace("$UID", Environment.GetEnvironmentVariable("UID"))
                                .Replace("$PASSWORD", Environment.GetEnvironmentVariable("PASSWORD"));
        
        services.AddDbContext<ProductDbContext>(options =>
        {                        
            options.UseMySQL(connectionString);
        });

        services.AddScoped<IProductRepository, ProductsRepository>();

        return services;
    }
}