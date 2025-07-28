using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data;

namespace eCommerce.Infrastructure.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;

    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        string? connectionStringTemplate = _configuration.GetConnectionString("PostgresConnection")!;
        string connectionString = connectionStringTemplate
                                  .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
                                  .Replace("$POSTGRES_PORT", Environment.GetEnvironmentVariable("POSTGRES_PORT"))
                                  .Replace("$POSTGRES_DATABASE", Environment.GetEnvironmentVariable("POSTGRES_DATABASE"))
                                  .Replace("$POSTGRES_USERNAME", Environment.GetEnvironmentVariable("POSTGRES_USERNAME"))
                                  .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));
        
        //Create a new NpgsqlConnection with the retrieved connection string
        _connection = new NpgsqlConnection(connectionString);   
    }

    public IDbConnection DbConnection => _connection;
}

