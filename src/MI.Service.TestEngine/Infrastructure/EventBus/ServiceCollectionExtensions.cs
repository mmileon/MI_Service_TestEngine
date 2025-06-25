using MI.Service.TestEngine.EventHandlers;
using MI.Service.TestEngine.Infrastructure.Configuration;
using MI.Service.Shared.MongoDb.Common.Extensions;
using MI.Service.Shared.MongoDb.Resources.Events;
using MI.Service.Shared.RabbitMQ;

namespace MI.Service.TestEngine.Infrastructure.EventBus;

/// <summary>
/// Dependency resolve for event service.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the external services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventBus(x => x.UseRabbitMQ(configuration.GetRabbitMqModel())
            .Subscribe<TestEngineActionResponse, TestEventHandler>());

        services.RegisterTestEngineSubmitEventPublisherService();
    }
}
