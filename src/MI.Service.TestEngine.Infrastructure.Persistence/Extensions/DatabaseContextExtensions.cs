using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence.Constants;

namespace MI.Service.TestEngine.Infrastructure.Persistence.Extensions;

/// <summary>
/// Provides extensions for database context.
/// </summary>
public static class DatabaseContextExtensions
{
    private const string ConnectionStrings = "ConnectionStrings";    
    private const string DatabaseProvider = "DatabaseProvider";

    /// <summary>
    /// Gets the effective provider name constant.
    /// </summary>
    /// <param name="databaseFacade">The database facade.</param>
    /// <returns></returns>
    public static string ProviderName(this DatabaseFacade databaseFacade)
    {
        return databaseFacade.IsNpgsql()
            ? DatabaseProviderName.PostgresSqlProviderName
            : DatabaseProviderName.SqlServerProviderName;
    }

    /// <summary>Gets the connection strings.</summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>
    ///   database connection string value.
    /// </returns>
    public static IConfigurationSection GetConnectionStrings(this IConfiguration configuration)
    {
        return configuration.GetSection(ConnectionStrings);
    }

    /// <summary>Gets the data base default connection model.</summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>DatabaseSettings</returns>
    public static DatabaseSettings GetDataBaseDefaultConnectionModel(this IConfiguration configuration)
    {
        return configuration.GetConnectionStrings().Get<DatabaseSettings>();
    }
    /// <summary>Gets the database provider.</summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IConfigurationSection GetDatabaseProvider(this IConfiguration configuration)
    {
        return configuration.GetSection(DatabaseProvider);
    }
    /// <summary>Gets the database provider model.</summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static DatabaseProvider GetDatabaseProviderModel(this IConfiguration configuration)
    {
        return configuration.GetDatabaseProvider().Get<DatabaseProvider>();
    }
}
