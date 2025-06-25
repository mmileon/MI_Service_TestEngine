using System.Reflection;
using MI.Service.TestEngine.Constants;
using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence;
using MI.Service.TestEngine.Initializers;
using MI.Service.Shared.AspNetCore.Swagger;
using MI.Service.Shared.Common.Extensions;

namespace MI.Service.TestEngine.Infrastructure;

/// <summary>
/// Dependency resolve for event service.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Initializes the database.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="environment">The environment.</param>
    public static void InitializeDatabase(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddTransient<IDatabaseEntriesInitializer, DatabaseEntriesInitializer>();
        services.AddTransient<IDataSeedInitializer, DataSeedInitializer>();
        services.AddTransient<IEntityReader, EntityReader>();

        if (environment.EnvironmentName != MainConstants.Swagger)
        {
            var serviceProvider = services.BuildServiceProvider();
            var databaseInitializer = serviceProvider.GetService<IDatabaseEntriesInitializer>();
            databaseInitializer?.InitializeDatabaseEntries(serviceProvider).GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// Adds the custom swagger related service configuration.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The application configuration provider.</param>
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var configurationSection = configuration.GetSwagger();
        services.AddCustomSwagger(configurationSection, assemblyName);
        services.AddSwaggerGenNewtonsoftSupport();
    }

    /// <summary>
    /// Adds the custom health checks for application.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="webHostEnvironment">The environment.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddCustomHealthChecks(
        this IServiceCollection services,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration)
    {
        if (!webHostEnvironment.EnvironmentName.EqualsInvariantIgnoreCase(MainConstants.Swagger))
        {
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>()
                .AddMongoDb(configuration.GetMongoModel().ConnectionString);
        }
    }
}
