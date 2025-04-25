using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.DbContext;

public class ProductDbContext 
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;

    public ProductDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        string? connectionString = _configuration["ConnectionStrings:MySQLConnection"];

        _connection = new MySqlConnection(connectionString);
    }

    public IDbConnection DbConnection=> _connection;
}
