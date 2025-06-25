using Microsoft.EntityFrameworkCore;
using MI.Service.TestEngine.Constants;
using MI.Service.TestEngine.Infrastructure.Persistence;

namespace MI.Service.TestEngine.Initializers;

/// <inheritdoc />
public class DatabaseEntriesInitializer : IDatabaseEntriesInitializer
{
    /// <inheritdoc />
    public async Task InitializeDatabaseEntries(IServiceProvider serviceProvider)
    {
        if (Environment.GetEnvironmentVariables().Contains(MainConstants.SeedCommandEnvironmentName) ||
            (Environment.GetEnvironmentVariables().Contains(MainConstants.GlobalMigrationEnvironmentName)
             && Environment.GetEnvironmentVariable(MainConstants.GlobalMigrationEnvironmentName).Equals("true")))
        {
            await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            try
            {
                var dataSeedInitializer = serviceProvider.GetRequiredService<IDataSeedInitializer>();
                await dataSeedInitializer.InitializeSeedData(serviceProvider);
            }
            finally
            {
                await context.DisposeAsync();
            }

            Environment.Exit(default);
        }

        if (Environment.GetEnvironmentVariables().Contains(MainConstants.GlobalMigrationEnvironmentName))
        {
            Environment.Exit(default);
        }
    }

    /// <summary>Imports the applications.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="accountId"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ImportSeedData(IServiceProvider serviceProvider, string accountId)
    {
        var dataSeedInitializer = serviceProvider.GetRequiredService<IDataSeedInitializer>();
        await dataSeedInitializer.ImportSeedData(serviceProvider, accountId);
    }
}
