using Microsoft.Extensions.DependencyInjection.Extensions;
using MI.Service.TestEngine.Business.Applications;
using MI.Service.TestEngine.Business.TestEventLogs;
using MI.Service.TestEngine.Business.Tests;
using MI.Service.TestEngine.Business.Documents;
using MI.Service.TestEngine.Business.MenuItems;
using MI.Service.TestEngine.Business.Messages;
using MI.Service.TestEngine.Business.RuleEventLogs;
using MI.Service.TestEngine.Business.Rules;
using MI.Service.TestEngine.Business.Users;
using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.Shared.AspNetCore.Cors;
using MI.Service.Shared.MongoDb.Common.Extensions;

namespace MI.Service.TestEngine.Infrastructure.DependencyInjections;

/// <summary>
/// Dependency resolve for Services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registration dependencies for application.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();


        services.TryAddTransient<ApplicationService>();
        services.TryAddTransient<ApplicationFetcher>();
        services.TryAddTransient<ApplicationUpsert>();
        services.TryAddTransient<ApplicationRemover>();
        services.TryAddTransient<ModuleFetcher>();
        services.TryAddTransient<ModuleUpsert>();
        services.TryAddTransient<ModuleRemover>();
        services.TryAddTransient<ApplicationModuleFetcher>();
        services.TryAddTransient<ApplicationModuleUpsert>();
        services.TryAddTransient<TestAttachmentService>();

        
    }

    /// <summary>
    /// Adds the database metadata service infrastructure to the service collection.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    public static void AddDatabaseMetadata(this IServiceCollection services)
    {
        services.RegisterDatabaseMetadataStorage();
    }

    /// <summary>
    /// Adds the Mongo registration.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configuration">The application configuration provider.</param>
    public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegistrationMongoAdapter(options =>
        {
            options.ConnectionString = configuration.GetMongoModel().ConnectionString;
            options.MaxConnectionPoolSize = configuration.GetMongoModel().MaxConnectionPoolSize;
            options.MinConnectionPoolSize = configuration.GetMongoModel().MinConnectionPoolSize;
        });
    }

    /// <summary>
    /// Adds the cors config for application.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configuration">The application configuration manager.</param>
    public static void AddCors(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCustomCors(configuration.GetCors());
    }
}
