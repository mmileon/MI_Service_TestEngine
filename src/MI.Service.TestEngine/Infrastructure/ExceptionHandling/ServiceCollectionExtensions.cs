using Newtonsoft.Json;
using MI.Service.TestEngine.Contracts;
using MI.Service.TestEngine.Shared.Exceptions;
using MI.Service.Shared.Constants.Common;
using MI.Service.Shared.ExceptionHandling.Exceptions.ValidationExceptions;

namespace MI.Service.TestEngine.Infrastructure.ExceptionHandling;

/// <summary>
/// Provides extension methods for <see cref="IApplicationBuilder"/> to add custom exception handling.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds custom exception handling to the application request handling pipeline.
    /// </summary>
    /// <param name="app">The application request handling pipeline builder.</param>
    public static void AddCustomExceptionHandling(this IApplicationBuilder app)
    {
        var hostingEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        var isDevelopment = hostingEnvironment.IsDevelopment();

        app.UseGlobalExceptionHandler(x =>
        {
            ConfigureResponseBody(x, hostingEnvironment);
            ConfigureExceptionBehavior(x, isDevelopment);
        });
        app.UseExceptionLogging();
    }

    private static void ConfigureResponseBody(
        ExceptionHandlerConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        var isDevelopment = hostingEnvironment.IsDevelopment();

        ErrorResponse ExceptionFormatter(Exception e, HttpContext httpContext)
        {
            return ProvideBaseError(e, httpContext, isDevelopment);
        }

        configuration.ObjectResponseBody(ExceptionFormatter);
    }

    private static void ConfigureExceptionBehavior(
        ExceptionHandlerConfiguration configuration,
        bool isDevelopment)
    {
        configuration.ContentType = MediaTypeNamesConstants.Json;

        configuration.Map<ArgumentMissingException>()
            .ToStatusCode(StatusCodes.Status400BadRequest)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment, ex.ErrorCode)));

        configuration.Map<ConflictException>()
            .ToStatusCode(StatusCodes.Status409Conflict)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment, ex.ErrorCode)));

        configuration.Map<DataNotFoundException>()
            .ToStatusCode(StatusCodes.Status404NotFound)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment, ex.ErrorCode)));

        configuration.Map<EntityNotFoundException>()
            .ToStatusCode(StatusCodes.Status404NotFound)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment, ex.ErrorCode)));

        configuration.Map<InvalidBusinessException>()
            .ToStatusCode(StatusCodes.Status404NotFound)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment, ex.ErrorCode)));

        configuration.Map<NoDataException>()
            .ToStatusCode(StatusCodes.Status400BadRequest)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment)));

        configuration.Map<BusinessValidationException>()
            .ToStatusCode(StatusCodes.Status422UnprocessableEntity)
            .WithBody((ex, context) => JsonConvert.SerializeObject(ProvideBaseError(ex, context, isDevelopment)));
    }

    private static void UseExceptionLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionLoggingMiddleware>();
    }

    private static ErrorResponse ProvideBaseError(Exception ex, HttpContext httpContext, bool isDevelopment, int? errorCode = null)
    {
        return new ErrorResponse
        {
            Messages = new[] { ex.Message },
            Code = errorCode,
            DeveloperMessage = isDevelopment ? ex.ToString() : null,
        };
    }
}
