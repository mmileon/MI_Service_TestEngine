using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MI.Service.TestEngine.Infrastructure.Persistence;
using MI.Service.TestEngine.Infrastructure.Persistence.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence.Constants;
using MI.Service.TestEngine.Infrastructure.Persistence.Extensions;
using MI.Service.TestEngine.Shared.Context;
using MI.Service.Shared.EntityFramework;

namespace MI.Service.TestEngine;

/// <summary>ApplicationDesignTimeDBContextFactory</summary>
public class ApplicationDesignTimeDBContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{

    /// <summary>Creates a new instance of a derived context.</summary>
    /// <param name="args">Arguments provided by the design-time service.</param>
    /// <returns>An instance of <span class="typeparameter">TContext</span>.</returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var configDb = configuration.GetDataBaseDefaultConnectionModel();
        var databaseProvider = configuration.GetDatabaseProviderModel();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServerMigrationGeneratorWithVersionTracker();

        builder = UseDatabase(builder, databaseProvider, configDb);

        var options = (DbContextOptions<ApplicationDbContext>)builder.Options;

        return new ApplicationDbContext(options,new AccountContext(),null);
    }

    private static DbContextOptionsBuilder UseDatabase(
        DbContextOptionsBuilder builder,
        DatabaseProvider databaseProvider,
        DatabaseSettings settings)
    {
        var assemblyPrefix = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

        return databaseProvider.Name switch
        {
            DatabaseProviderName.PostgresSqlProviderName => builder.UseNpgsql(
                    settings.PostgreSQLTestEngineDatabase,
                    builder => builder.MigrationsAssembly($"{assemblyPrefix}.{DatabaseProviderName.PostgresSqlProviderName}"))
                .UseNpgsqlMigrationGeneratorWithVersionTracker(),
            _ => builder.UseSqlServer(
                    settings.SqlServerTestEngineDatabase,
                    builder => builder.MigrationsAssembly($"{assemblyPrefix}.{DatabaseProviderName.SqlServerProviderName}"))
                .UseSqlServerMigrationGeneratorWithVersionTracker(),
        };
    }
}