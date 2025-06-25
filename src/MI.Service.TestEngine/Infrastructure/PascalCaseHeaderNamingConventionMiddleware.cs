using System.Text.RegularExpressions;
using Microsoft.Extensions.Primitives;
using MI.Service.Shared.Common.Extensions;

namespace MI.Service.TestEngine.Infrastructure;

/// <summary>
/// Move headers all headers in pascal case by naming convention.
/// </summary>
public class PascalCaseHeaderNamingConventionMiddleware
{
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="PascalCaseHeaderNamingConventionMiddleware"/> class.
    /// </summary>
    /// <param name="next">Next pipeline for request delegate.</param>
    public PascalCaseHeaderNamingConventionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// Invoke request headers description.
    /// </summary>
    /// <param name="context">The http request context.</param>
    /// <returns>Task for pipeline direction.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var modifiedHeaders = context.Request.Headers
            .Select(p => new KeyValuePair<string, StringValues>(
                ToPascalCase(p.Key, "-", "-"),
                p.Value))
            .ToDictionary(k => k.Key, v => v.Value);

        context.Request.Headers.Clear();
        context.Request.Headers.TryAddRange(modifiedHeaders);

        await this.next(context);
    }

    private static string ToPascalCase(string original, string originalDelimiter, string resultDelimiter)
    {
        var startsWithLowerCaseChar = new Regex("^[a-z]");
        var pascalCase = original.Split(new[] { originalDelimiter }, StringSplitOptions.RemoveEmptyEntries)
            .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()));

        return string.Join(resultDelimiter, pascalCase);
    }
}
