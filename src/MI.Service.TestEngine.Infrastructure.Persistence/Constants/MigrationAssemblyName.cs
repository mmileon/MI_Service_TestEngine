namespace MI.Service.TestEngine.Infrastructure.Persistence.Constants;

/// <summary>
/// Provides migration assembly names.
/// </summary>
public static class MigrationAssemblyName
{
    /// <summary>
    /// Provides SQL Server assembly name.
    /// </summary>
    public static string SqlServerAssemblyName = $"{typeof(ApplicationDbContext).Assembly.GetName().Name}.MSSQL";

    /// <summary>
    /// Provides postgres sql assembly name.
    /// </summary>
    public static string PostgresSqlAssemblyName = $"{typeof(ApplicationDbContext).Assembly.GetName().Name}.PostgreSQL";
}
