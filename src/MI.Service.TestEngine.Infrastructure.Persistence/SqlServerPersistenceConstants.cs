namespace MI.Service.TestEngine.Infrastructure.Persistence;

/// <summary>
/// Provides constants for SQL server persistence storage.
/// </summary>
public static class PersistenceStorageConstants
{
    /// <summary>
    /// Provides SQL Server assembly name.
    /// </summary>
    public static string SqlServerAssemblyName = $"{typeof(ApplicationDbContext).Assembly.GetName().Name}.MSSQL";
}
