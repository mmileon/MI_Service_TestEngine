using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.TestEngine.Infrastructure.Persistence.Extensions;
using MI.Service.Shared.AspNetCore.Cors;
using MI.Service.Shared.Auth;
using MI.Service.Shared.Http;

namespace MI.Service.TestEngine.Infrastructure.Authentication;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> for SourceService configuration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the authentication config for application.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configuration">The application configuration provider.</param>
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureHttp(configuration.GetAuthentication())
            .AddCustomAuthentication(configuration.GetAuthentication());
    }

    /// <summary>
    /// Adds the cors config for application.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configuration">The application configuration provider.</param>
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomCors(configuration.GetCors());
    }
}
