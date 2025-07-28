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
                                .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
                                .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE"))
                                .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
                                .Replace("$MYSQL_UID", Environment.GetEnvironmentVariable("MYSQL_UID"))
                                .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));
        
        services.AddDbContext<ProductDbContext>(options =>
        {                        
            options.UseMySQL(connectionString);
        });

        services.AddScoped<IProductRepository, ProductsRepository>();

        return services;
    }
}

