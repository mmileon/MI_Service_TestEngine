using MI.Service.TestEngine.AuthServiceApi.Configuration;
using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.TestEngine.NotificationServiceApi.Configuration;
using MI.Service.TestEngine.StorageServiceApi.Configuration;

namespace MI.Service.TestEngine.Infrastructure.ExternalServices;

/// <summary>
/// Dependency resolve for Services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the external services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotificationService(configuration.GetNotificationService(), configuration.GetAppConfiguration());
        services.AddAuthService(configuration.GetAuthService());
        services.AddStorageService(configuration.GetStorageService());
    }
}