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
        string? env = Environment.GetEnvironmentVariable("ConnectionStrings:MySQLDataConnection");
        string connectionStringTemplate = configuration["ConnectionStrings:MySQLDataConnection"]!;

        string connectionString = connectionStringTemplate
                                .Replace("$SERVER", Environment.GetEnvironmentVariable("SERVER"))
                                .Replace("$PORT", Environment.GetEnvironmentVariable("PORT"))
                                .Replace("$DATABASE", Environment.GetEnvironmentVariable("DATABASE"))
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
