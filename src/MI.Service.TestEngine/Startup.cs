using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MI.Service.TestEngine.Infrastructure;
using MI.Service.TestEngine.Infrastructure.Authentication;
using MI.Service.TestEngine.Infrastructure.AutoMapper;
using MI.Service.TestEngine.Infrastructure.DependencyInjections;
using MI.Service.TestEngine.Infrastructure.EventBus;
using MI.Service.TestEngine.Infrastructure.ExceptionHandling;
using MI.Service.TestEngine.Infrastructure.ExternalServices;
using MI.Service.TestEngine.Infrastructure.Language;
using MI.Service.TestEngine.Infrastructure.LoggingHandling;
using MI.Service.TestEngine.Infrastructure.Mvc;
using MI.Service.TestEngine.Infrastructure.OpenTelemetry;
using MI.Service.TestEngine.Infrastructure.Persistence.Extensions;
using MI.Service.TestEngine.Infrastructure.ResourceAuthorization;
using MI.Service.TestEngine.MultiTenancy;
using MI.Service.Shared.AspNetCore.Extensions;
using MI.Service.Shared.AspNetCore.Headers.Extensions;
using MI.Service.Shared.AspNetCore.HealthChecks;
using MI.Service.Shared.AspNetCore.Logging.Extensions;
using MI.Service.Shared.AspNetCore.Swagger;
using MI.Service.Shared.Common.Multitenancy.Common;
using MI.Service.Shared.MongoDb.Common.Extensions;

namespace MI.Service.TestEngine;

/// <summary>
/// The startup class for the application.
/// </summary>
public class Startup
{
    private readonly IWebHostEnvironment hostingEnvironment;

    /// <summary>
    /// Gets or sets the application configuration.
    /// </summary>
    public IConfiguration Configuration { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="hostingEnvironment">The hosting environment.</param>
    public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        this.hostingEnvironment = hostingEnvironment;
        this.Configuration = new ConfigurationBuilder()
            .AddConfiguration(configuration)
            .AddJsonFile("configuration/appsettings.custom.json", true, false)
            .AddJsonFile("configuration/appsettings.custom-Test-engine.json", true, false)
            .Build();
    }

    /// <summary>
    /// Configures the application services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        this.Configuration = this.Configuration.BuildCustomConfigurationWithSecrets();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddAccountContext();
        services.AddRequiredSecurityHeaders();
        services.AddDataProtection();
        services.AddMongo(this.Configuration);
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });
        services.AddCustomOptions(this.Configuration);
        services.AddSingleton(this.Configuration);
        services.AddEndpointsApiExplorer();
        services.AddCustomMvc();
        services.AddAuthentication(this.Configuration);
        services.ConfigureResourceAuthorization();
        services.AddDatabaseMetadata();
        services.AddMultitenancy();
        services.AddOptions().AddMultiTenancySetting();
        services.AddDependencyInjections();
        services.AddSwagger(this.Configuration);
        services.AddEntityFrameworkService(this.Configuration);
        services.AddCustomAutoMapper();
        services.AddEventBus(this.Configuration);
        services.InitializeDatabase(this.hostingEnvironment);
        services.AddCustomHealthChecks(this.hostingEnvironment, this.Configuration);
        services.RegisterMaintenanceModeStorage();
        services.ConfigureNLog(this.Configuration);        
        services.AddOpenTelemetry(this.Configuration);
        services.AddCors(this.Configuration);
        services.AddExternalServices(this.Configuration);
        services.RegisterAccountManagementStorage();
        services.AddTranslation();
    }

    /// <summary>
    /// Configures the application request handling pipeline.
    /// </summary>
    /// <param name="applicationBuilder">The application request pipeline configurator.</param>
    /// <param name="apiDescriptionGroupCollectionProvider">The API description group collection provider.</param>
    public void Configure(
        IApplicationBuilder applicationBuilder,
        IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
    {
        applicationBuilder.UseWithDefaultSecurityHeaders();
        applicationBuilder.UseMiddleware<PascalCaseHeaderNamingConventionMiddleware>();
        applicationBuilder.ConfigureBasePath();
        applicationBuilder.UseCustomSwagger(apiDescriptionGroupCollectionProvider);
        applicationBuilder.UseStaticFiles();
        applicationBuilder.AddCustomExceptionHandling();
        applicationBuilder.UseAuthentication();
        applicationBuilder.UseMiddleware<AccountResolutionMiddleware>();
        applicationBuilder.UseMultitenancy();
        applicationBuilder.UseDefaultHealthChecks();    
        applicationBuilder.UseCors();
        applicationBuilder.UseRouting();
        applicationBuilder.UseRequestLogging();
        applicationBuilder.UseResponseLogging();
        applicationBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
