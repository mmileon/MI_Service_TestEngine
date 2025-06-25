using System.CommandLine;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NLog.Extensions.Logging;
using MI.Service.TestEngine.Infrastructure.ApplicationStartup;
using MI.Service.Shared.AspNetCore.Swagger;

namespace MI.Service.TestEngine;

/// <summary>
/// The class which contains entry point of the application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        var host = CreateWebHostBuilder(args).Build();

        var rootCommand = new RunApplicationCommand(host);
        rootCommand.AddCommand(new GenerateSwaggerFileCommand(host));
        rootCommand.Invoke(args);
    }

    private static IHostBuilder CreateWebHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseKestrel(options =>
                    {
                        options.AddServerHeader = false;
                        options.Limits.MaxRequestBodySize = long.MaxValue;
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10.0);
                        options.Limits.MinRequestBodyDataRate = new MinDataRate(100, TimeSpan.FromSeconds(10));
                    })
                    .ConfigureLogging(config =>
                    {
                        config.ClearProviders();
                        config.AddNLog();
                    })
                    .UseStartup<Startup>();
            });
    }
}
