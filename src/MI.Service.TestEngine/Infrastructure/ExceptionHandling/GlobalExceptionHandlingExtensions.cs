using GlobalExceptionHandler.ContentNegotiation.Mvc;
using GlobalExceptionHandler.WebApi;

namespace MI.Service.TestEngine.Infrastructure.ExceptionHandling;

/// <summary>
/// Provides extension methods for <see cref="ExceptionHandlerConfiguration"/>.
/// </summary>
public static class GlobalExceptionHandlingExtensions
{
    /// <summary>
    /// Specifies response formatter with data transfer object to be used for global exception handling.
    /// </summary>
    /// <param name="configuration">The exception handler configuration.</param>
    /// <param name="formatter">The error formatter.</param>
    /// <typeparam name="T">The type of data transfer object to be returned.</typeparam>
    public static void ObjectResponseBody<T>(
        this ExceptionHandlerConfiguration configuration,
        Func<Exception, HttpContext, T> formatter)
    {
        Task Formatter(
            Exception exception,
            HttpContext httpContext,
            HandlerContext handlerContext)
        {
            httpContext.Response.ContentType = string.Empty;
            httpContext.WriteAsyncObject(formatter(exception, httpContext));

            return Task.CompletedTask;
        }

        configuration.ResponseBody(Formatter);
    }
}
