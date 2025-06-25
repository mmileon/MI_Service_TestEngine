using System.Text;

namespace MI.Service.TestEngine.Shared.Utility;

/// <summary>
/// Text Helper
/// </summary>
public static class TextHelper
{
    /// <summary>
    /// Converts to querystring.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public static string ToQueryString(this Dictionary<string, List<string>> parameters)
    {
        var stringBuilder = new StringBuilder();

        foreach (var param in parameters)
        {
            var values = param.Value.Distinct().ToList();
            foreach (var value in values)
            {
                stringBuilder.Append($"{param.Key}={value}&");
            }
        }

        return stringBuilder.ToString().TrimEnd('&');
    }

    /// <summary>
    /// Converts to querystring.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public static string ToQueryString(this Dictionary<string, object> parameters)
    {
        var stringBuilder = new StringBuilder();

        foreach (var parameter in parameters)
        {
            stringBuilder.Append($"{parameter.Key}={parameter.Value}&");
        }

        return stringBuilder.ToString().TrimEnd('&');
    }
}