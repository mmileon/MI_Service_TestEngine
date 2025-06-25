namespace MI.Service.TestEngine.Constants;

/// <summary>
/// Provides project level constants.
/// </summary>
public static class MainConstants
{
    /// <summary>
    /// Provides environment name for swagger.
    /// </summary>
    public const string Swagger = "swagger";

    /// <summary>
    /// The environment name.
    /// </summary>
    public const string TestingEnvironmentName = "IntegrationTesting";

    /// <summary>
    /// The environment variable for initialize database.
    /// </summary>
    public const string SeedCommandEnvironmentName = "SEED_APR_DATABASE";

    /// <summary>
    /// Global Migration environment on App Startup.
    /// </summary>
    public const string GlobalMigrationEnvironmentName = "RUN_MIGRATION_ON_STARTUP";
}