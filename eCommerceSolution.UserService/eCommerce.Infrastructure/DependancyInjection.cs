using eCommerce.Core.RespositoryContracts;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure;

public static class DependancyInjection
{
    /// <summary>
    /// Extension method to add Infrastructure services to the dependancy injection container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddSingleton<DapperDbContext>();
        return services;
    }
}