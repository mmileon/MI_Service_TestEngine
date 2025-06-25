using MI.Service.Shared.Constants.Common;
using MI.Service.Shared.MongoDb.Common.Extensions;

namespace MI.Service.TestEngine.Infrastructure.ResourceAuthorization;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add resource authorization services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configure the resource authorization.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void ConfigureResourceAuthorization(this IServiceCollection services)
    {
        services.RegisterResourcesStorage(p =>
        {
            p.SupportedApplications = new[]
            {
                ApplicationClientConstants.TestEngine,
            };
        });
    }
}