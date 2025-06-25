using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.Shared.SecretConfigurationApplier.Configuration;
using MI.Service.Shared.SecretConfigurationApplier.Extensions;

namespace MI.Service.TestEngine.Infrastructure;

/// <summary>
/// Provides extension methods for <see cref="Microsoft.AspNetCore.Hosting.IHostingEnvironment"/> for request handling pipeline configuration.
/// </summary>
public static class HostingEnvironmentExtensions
{
    /// <summary>
    /// Builds the custom configuration by specified environment.
    /// </summary>
    /// <param name="configuration">The configuration settings.</param>
    /// <returns>Configuration for application by specified environment.</returns>
    public static IConfiguration BuildCustomConfigurationWithSecrets(
        this IConfiguration configuration)
    {
        var secretManagerSetting = configuration.GetSecretsManagerSection().Get<SecretManagerSettings>();

        return new ConfigurationBuilder()
            .AddConfiguration(configuration)
            .AddSecretManagerConfiguration(
                configuration,
                new SecretManagerSettings
                {
                    EnableSecretManager = secretManagerSetting.EnableSecretManager,
                    SecretManagerApiUrl = secretManagerSetting.SecretManagerApiUrl,
                })
            .Build();
    }
}
