using MI.Service.Shared.AspNetCore.Logging.Middlewares;

namespace MI.Service.TestEngine.Infrastructure.LoggingHandling;

/// <summary>
///   A middleware that will log request/response to the api.
/// </summary>
public static class InputLoggingMiddlewareExtensions
{
    /// <summary>Uses the request logging.</summary>
    /// <param name="builder">The builder.</param>
    /// <returns>
    /// A reference to this instance after the operation has completed.
    /// </returns>
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingRequestMiddleware>();
    }

    /// <summary>Uses the response logging.</summary>
    /// <param name="builder">The builder.</param>
    /// <returns>
    /// A reference to this instance after the operation has completed.
    /// </returns>
    public static IApplicationBuilder UseResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingResponseMiddleware>();
    }
}