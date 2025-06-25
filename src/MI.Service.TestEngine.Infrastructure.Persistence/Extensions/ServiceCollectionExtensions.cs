using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MI.Service.TestEngine.Domain.Repositories;
using MI.Service.TestEngine.Infrastructure.Persistence.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence.Constants;
using MI.Service.TestEngine.Infrastructure.Persistence.Repositories;

namespace MI.Service.TestEngine.Infrastructure.Persistence.Extensions;

/// <summary>
/// Provides EntityFrameworkCore dependencies for Test engine.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the entity framework service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddEntityFrameworkService(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultDb = configuration.GetDataBaseDefaultConnectionModel();
        var databaseProvider = configuration.GetDatabaseProviderModel();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseDatabase(databaseProvider, defaultDb);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
       

        return services;
    }

    private static DbContextOptionsBuilder UseDatabase(
            this DbContextOptionsBuilder builder,
            DatabaseProvider databaseProvider,
            DatabaseSettings dbSettings)
    {
        return databaseProvider.Name switch
        {
            
            _ => throw new Exception($"Unsupported provider: {databaseProvider.Name}")
        };
    }
}
