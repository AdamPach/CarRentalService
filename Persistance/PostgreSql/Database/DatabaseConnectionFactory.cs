using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CarRentalService.Persistence.PostgreSql.Database;

public class DatabaseConnectionFactory : IDisposable
{
    private readonly NpgsqlDataSource _dataSource;
    
    public DatabaseConnectionFactory(IConfiguration configuration)
    {
        if (configuration.GetConnectionString("PostgreSql") is null)
        {
            throw new ArgumentNullException("PostgreSql connection string is missing in appsettings.json");
        }

        _dataSource = NpgsqlDataSource.Create(configuration.GetConnectionString("PostgreSql")!);
    }
    
    public async Task<NpgsqlConnection> CreateConnection()
    {
        return await _dataSource.OpenConnectionAsync();
    }

    public void Dispose()
    {
        _dataSource.Dispose();
    }
}